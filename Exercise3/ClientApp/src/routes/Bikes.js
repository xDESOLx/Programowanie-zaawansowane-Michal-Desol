import { Box, Heading, IconButton, Spinner, Table, TableContainer, Tbody, Td, Th, Thead, Tr, useToast } from "@chakra-ui/react";
import useBikes from "../hooks/useBikes";
import { FormikProvider, useFormik } from "formik";
import { bikeValidationSchema } from "../schemas/bikeValidationSchema";
import FormControl from "../components/FormControl";
import BikeTableRow from "../components/BikeTableRow"
import { bikeTypes } from "../lib/bikeTypes";
import { AddIcon } from "@chakra-ui/icons";
import { useTranslation } from "react-i18next";

export default function Bikes() {
    const { bikes, isLoading, mutate } = useBikes()
    const toast = useToast()

    const formik = useFormik({
        initialValues: {
            type: '',
            frameSize: '',
            wheelSize: '',
            color: ''
        },
        validationSchema: bikeValidationSchema,
        onSubmit: async (values, { resetForm }) => {
            const result = await fetch('/api/bikes', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(values)
            })
            if (result.ok) {
                resetForm()
                mutate([...bikes, await result.json()])
                toast({
                    title: 'Utworzono rower.',
                    status: 'success',
                    isClosable: true,
                })
            } else {
                toast({
                    title: 'Wystąpił problem podczas dodawania roweru.',
                    status: "error",
                    isClosable: true,
                })
            }
        }
    })

    const handleDelete = async id => {
        const result = await fetch(`/api/bikes/${id}`, {
            method: 'DELETE',
        })
        if (result.ok) {
            mutate(bikes.filter(bike => bike.id !== id))
            toast({
                title: 'Rower został usunięty.',
                status: 'success',
                isClosable: true,
            })
        } else {
            toast({
                title: 'Wystąpił problem podczas usuwania roweru.',
                status: "error",
                isClosable: true,
            })
        }
    }

    const handleEdit = async values => {
        const result = await fetch(`/api/bikes/${values.id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(values)
        })
        if (result.ok) {
            mutate(bikes.map(bike => {
                if (bike.id === values.id) {
                    return values
                } else {
                    return bike
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
            <Heading mb={"4"} as={"h1"}>{t('Rowery')} {isLoading && <Spinner />}</Heading>
            {!isLoading && <TableContainer height={"100%"}>
                <Table size={"lg"}>
                    <Thead>
                        <Tr>
                            <Th>#</Th>
                            <Th>{t('Typ')}</Th>
                            <Th>{t('Rozmiar ramy')}</Th>
                            <Th>{t('Rozmiar kół')}</Th>
                            <Th>{t('Kolor')}</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <FormikProvider value={formik}>
                            <Tr>
                                <Td>
                                    <form onSubmit={formik.handleSubmit} id="addBikeForm"></form>
                                </Td>
                                <Td>
                                    <FormControl
                                        field="type"
                                        form="addBikeForm"
                                        type="select"
                                        placeholder={t('Wybierz typ...')}
                                        options={Object.entries(bikeTypes).map(([value, label]) => ({ label, value }))}
                                    />
                                </Td>
                                <Td>
                                    <FormControl field="frameSize" form="addBikeForm" type="number" min={0} max={99.99} />
                                </Td>
                                <Td>
                                    <FormControl field="wheelSize" form="addBikeForm" type="number" min={0} max={99.99} />
                                </Td>
                                <Td>
                                    <FormControl field="color" form="addBikeForm" type="text" />
                                </Td>
                                <Td><IconButton isDisabled={formik.isSubmitting} type="submit" form="addBikeForm" colorScheme="green" icon={<AddIcon />} /></Td>
                            </Tr>
                        </FormikProvider>
                        {bikes.map(bike => <BikeTableRow key={bike.id} bike={bike} onDelete={handleDelete} onEdit={handleEdit} />)}
                    </Tbody>
                </Table>
            </TableContainer>}
        </Box>
    )
}