import { Box, Heading, IconButton, Skeleton, Spinner, Table, TableContainer, Tbody, Td, Th, Thead, Tr, useToast } from "@chakra-ui/react";
import useBikeRentals from "../hooks/useBikeRentals";
import { bikeRentalValidationSchema } from "../schemas/bikeRentalValidationSchema";
import { FormikProvider, useFormik } from "formik";
import FormControl from "../components/FormControl";
import { AddIcon } from "@chakra-ui/icons";
import BikeRentalTableRow from "../components/BikeRentalTableRow";
import usePeople from "../hooks/usePeople";
import useBikes from "../hooks/useBikes";
import { bikeTypes } from "../lib/bikeTypes";
import { format } from "date-fns";
import { useTranslation } from "react-i18next";

export default function BikeRentals() {
    const { bikeRentals, isLoading, mutate } = useBikeRentals()
    const { people, isLoading: isLoadingPeople } = usePeople()
    const { bikes, isLoading: isLoadingBikes } = useBikes()
    const toast = useToast()

    const formik = useFormik({
        initialValues: {
            personId: '',
            bikeId: '',
            rentalDate: format(new Date(), 'yyyy-MM-dd'),
        },
        validationSchema: bikeRentalValidationSchema,
        onSubmit: async (values, { resetForm }) => {
            const result = await fetch('/api/bikeRentals', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(values)
            })
            if (result.ok) {
                resetForm()
                mutate([...bikeRentals, await result.json()])
                toast({
                    title: 'Utworzono wypożyczenie roweru.',
                    status: 'success',
                    isClosable: true,
                })
            } else {
                toast({
                    title: 'Wystąpił problem podczas dodawania wypożyczenia roweru.',
                    status: "error",
                    isClosable: true,
                })
            }
        }
    })

    const handleDelete = async id => {
        const result = await fetch(`/api/bikeRentals/${id}`, {
            method: 'DELETE',
        })
        if (result.ok) {
            mutate(bikeRentals.filter(bikeRental => bikeRental.id !== id))
            toast({
                title: 'Wypożyczenie roweru zostało usunięte.',
                status: 'success',
                isClosable: true,
            })
        } else {
            toast({
                title: 'Wystąpił problem podczas usuwania wypożyczenia roweru.',
                status: "error",
                isClosable: true,
            })
        }
    }

    const handleEdit = async values => {
        const result = await fetch(`/api/bikeRentals/${values.id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(values)
        })
        if (result.ok) {
            mutate(bikeRentals.map(bikeRental => {
                if (bikeRental.id === values.id) {
                    return values
                } else {
                    return bikeRental
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
            <Heading mb={"4"} as={"h1"}>{t('Wypożyczenia rowerów')} {isLoading && <Spinner />}</Heading>
            {!isLoading && <TableContainer height={"100%"}>
                <Table size={"lg"}>
                    <Thead>
                        <Tr>
                            <Th>#</Th>
                            <Th>{t('Osoba')}</Th>
                            <Th>{t('Rower')}</Th>
                            <Th>{t('Data wypożyczenia')}</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <FormikProvider value={formik}>
                            <Tr>
                                <Td>
                                    <form onSubmit={formik.handleSubmit} id="addBikeRentalForm"></form>
                                </Td>
                                <Td>
                                    <Skeleton isLoaded={!isLoadingPeople}>
                                        <FormControl field="personId" form="addBikeRentalForm" type="select" placeholder={t('Wybierz osobę...')} options={people?.map(person => ({
                                            label: `${person.firstName} ${person.lastName}`,
                                            value: person.id
                                        }))} />
                                    </Skeleton>
                                </Td>
                                <Td>
                                    <Skeleton isLoaded={!isLoadingBikes}>
                                        <FormControl field="bikeId" form="addBikeRentalForm" type="select" placeholder={t('Wybierz rower...')} options={bikes?.map(bike => ({
                                            label: `${bikeTypes[bike.type]} ${bike.color}`,
                                            value: bike.id
                                        }))} />
                                    </Skeleton>
                                </Td>
                                <Td>
                                    <FormControl field="rentalDate" form="addBikeRentalForm" type="date" />
                                </Td>
                                <Td><IconButton isDisabled={formik.isSubmitting} type="submit" form="addBikeRentalForm" colorScheme="green" icon={<AddIcon />} /></Td>
                            </Tr>
                        </FormikProvider>
                        {bikeRentals.map(bikeRental => <BikeRentalTableRow key={bikeRental.id} bikeRental={bikeRental} onDelete={handleDelete} onEdit={handleEdit} />)}
                    </Tbody>
                </Table>
            </TableContainer>}
        </Box>
    )
}