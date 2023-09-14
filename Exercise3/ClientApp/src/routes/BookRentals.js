import { Box, Heading, IconButton, Skeleton, Spinner, Table, TableContainer, Tbody, Td, Th, Thead, Tr, useToast } from "@chakra-ui/react";
import useBookRentals from "../hooks/useBookRentals";
import useBooks from "../hooks/useBooks";
import usePeople from "../hooks/usePeople";
import { FormikProvider, useFormik } from "formik";
import { bookRentalValidationSchema } from "../schemas/bookRentalValidationSchema";
import FormControl from "../components/FormControl";
import { AddIcon } from "@chakra-ui/icons";
import BookRentalTableRow from "../components/BookRentalTableRow";
import { format } from "date-fns";
import { useTranslation } from "react-i18next";

export default function BookRentals() {
    const { bookRentals, isLoading, mutate } = useBookRentals()
    const { people, isLoading: isLoadingPeople } = usePeople()
    const { books, isLoading: isLoadingBooks } = useBooks()
    const toast = useToast()

    const formik = useFormik({
        initialValues: {
            personId: '',
            bookId: '',
            rentalDate: format(new Date(), 'yyyy-MM-dd'),
        },
        validationSchema: bookRentalValidationSchema,
        onSubmit: async (values, { resetForm }) => {
            const result = await fetch('/api/bookRentals', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(values)
            })
            if (result.ok) {
                resetForm()
                mutate([...bookRentals, await result.json()])
                toast({
                    title: 'Utworzono wypożyczenie książki.',
                    status: 'success',
                    isClosable: true,
                })
            } else {
                toast({
                    title: 'Wystąpił problem podczas dodawania wypożyczenia książki.',
                    status: "error",
                    isClosable: true,
                })
            }
        }
    })

    const handleDelete = async id => {
        const result = await fetch(`/api/bookRentals/${id}`, {
            method: 'DELETE',
        })
        if (result.ok) {
            mutate(bookRentals.filter(bookRental => bookRental.id !== id))
            toast({
                title: 'Wypożyczenie książki zostało usunięte.',
                status: 'success',
                isClosable: true,
            })
        } else {
            toast({
                title: 'Wystąpił problem podczas usuwania wypożyczenia książki.',
                status: "error",
                isClosable: true,
            })
        }
    }

    const handleEdit = async values => {
        const result = await fetch(`/api/bookRentals/${values.id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(values)
        })
        if (result.ok) {
            mutate(bookRentals.map(bookRental => {
                if (bookRental.id === values.id) {
                    return values
                } else {
                    return bookRental
                }
            }))
            toast({
                title: 'Zmiany zostały zapisane.',
                status: 'success',
                isClosable: true,
            })
        } else {
            toast({
                title: 'Wystąpił problem podczas zapisywania zmian.',
                status: "error",
                isClosable: true,
            })
        }
    }

    const { t } = useTranslation()

    return (
        <Box height={"100%"}>
            <Heading mb={"4"} as={"h1"}>{t('Wypożyczenia książek')} {isLoading && <Spinner />}</Heading>
            {!isLoading && <TableContainer height={"100%"}>
                <Table size={"lg"}>
                    <Thead>
                        <Tr>
                            <Th>#</Th>
                            <Th>{t('Osoba')}</Th>
                            <Th>{t('Książka')}</Th>
                            <Th>{t('Data wypożyczenia')}</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <FormikProvider value={formik}>
                            <Tr>
                                <Td>
                                    <form onSubmit={formik.handleSubmit} id="addBookRentalForm"></form>
                                </Td>
                                <Td>
                                    <Skeleton isLoaded={!isLoadingPeople}>
                                        <FormControl field="personId" form="addBookRentalForm" type="select" placeholder={t('Wybierz osobę...')} options={people?.map(person => ({
                                            label: `${person.firstName} ${person.lastName}`,
                                            value: person.id
                                        }))} />
                                    </Skeleton>
                                </Td>
                                <Td>
                                    <Skeleton isLoaded={!isLoadingBooks}>
                                        <FormControl field="bookId" form="addBookRentalForm" type="select" placeholder={t('Wybierz książkę...')} options={books?.map(book => ({
                                            label: `${book.author} - ${book.title}`,
                                            value: book.id
                                        }))} />
                                    </Skeleton>
                                </Td>
                                <Td>
                                    <FormControl field="rentalDate" form="addBookRentalForm" type="date" />
                                </Td>
                                <Td><IconButton isDisabled={formik.isSubmitting} type="submit" form="addBookRentalForm" colorScheme="green" icon={<AddIcon />} /></Td>
                            </Tr>
                        </FormikProvider>
                        {bookRentals.map(bookRental => <BookRentalTableRow key={bookRental.id} bookRental={bookRental} onDelete={handleDelete} onEdit={handleEdit} />)}
                    </Tbody>
                </Table>
            </TableContainer>}
        </Box>
    )
}