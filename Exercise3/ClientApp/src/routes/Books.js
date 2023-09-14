import { Box, Heading, IconButton, Spinner, Table, TableContainer, Tbody, Td, Th, Thead, Tr, useToast } from "@chakra-ui/react"
import useBooks from "../hooks/useBooks"
import { bookValidationSchema } from "../schemas/bookValidationSchema"
import { FormikProvider, useFormik } from "formik"
import FormControl from "../components/FormControl"
import { AddIcon } from "@chakra-ui/icons"
import BookTableRow from "../components/BookTableRow"
import { useTranslation } from "react-i18next"

export default function Books() {
    const { books, isLoading, mutate } = useBooks()
    const toast = useToast()

    const formik = useFormik({
        initialValues: {
            title: '',
            author: '',
            genre: '',
            isbn: ''
        },
        validationSchema: bookValidationSchema,
        onSubmit: async (values, { resetForm }) => {
            const result = await fetch('/api/books', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(values)
            })
            if (result.ok) {
                resetForm()
                mutate([...books, await result.json()])
                toast({
                    title: 'Utworzono książkę.',
                    description: `${values.author} - ${values.title}`,
                    status: 'success',
                    isClosable: true,
                })
            } else {
                toast({
                    title: 'Wystąpił problem podczas dodawania książki.',
                    status: "error",
                    isClosable: true,
                })
            }
        }
    })

    const handleDelete = async id => {
        const result = await fetch(`/api/books/${id}`, {
            method: 'DELETE',
        })
        if (result.ok) {
            mutate(books.filter(book => book.id !== id))
            toast({
                title: 'Książka została usunięta.',
                status: 'success',
                isClosable: true,
            })
        } else {
            toast({
                title: 'Wystąpił problem podczas usuwania książki.',
                status: "error",
                isClosable: true,
            })
        }
    }

    const handleEdit = async values => {
        const result = await fetch(`/api/books/${values.id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(values)
        })
        if (result.ok) {
            mutate(books.map(book => {
                if (book.id === values.id) {
                    return values
                } else {
                    return book
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
            <Heading mb={"4"} as={"h1"}>{t('Książki')} {isLoading && <Spinner />}</Heading>
            {!isLoading && <TableContainer height={"100%"}>
                <Table size={"lg"}>
                    <Thead>
                        <Tr>
                            <Th>#</Th>
                            <Th>{t('Tytuł')}</Th>
                            <Th>{t('Autor')}</Th>
                            <Th>{t('Gatunek')}</Th>
                            <Th>{t('ISBN')}</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <FormikProvider value={formik}>
                            <Tr>
                                <Td>
                                    <form onSubmit={formik.handleSubmit} id="addBookForm"></form>
                                </Td>
                                <Td>
                                    <FormControl field="title" form="addBookForm" type="text" />
                                </Td>
                                <Td>
                                    <FormControl field="author" form="addBookForm" type="text" />
                                </Td>
                                <Td>
                                    <FormControl field="genre" form="addBookForm" type="text" />
                                </Td>
                                <Td>
                                    <FormControl field="isbn" form="addBookForm" type="text" />
                                </Td>
                                <Td><IconButton isDisabled={formik.isSubmitting} type="submit" form="addBookForm" colorScheme="green" icon={<AddIcon />} /></Td>
                            </Tr>
                        </FormikProvider>
                        {books.map(book => <BookTableRow key={book.id} book={book} onDelete={handleDelete} onEdit={handleEdit} />)}
                    </Tbody>
                </Table>
            </TableContainer>}
        </Box>
    )
}