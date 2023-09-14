import { Box, Heading, IconButton, Table, TableContainer, Tbody, Td, Th, Thead, Tr, useToast } from "@chakra-ui/react";
import { Spinner } from '@chakra-ui/react'
import FormControl from '../components/FormControl'
import PersonTableRow from '../components/PersonTableRow'
import usePeople from "../hooks/usePeople";
import { AddIcon } from "@chakra-ui/icons";
import { FormikProvider, useFormik } from "formik";
import { personValidationSchema } from "../schemas/personValidationSchema";
import { useTranslation } from "react-i18next";

export default function People() {
    const { people, isLoading, mutate } = usePeople()
    const toast = useToast()

    const formik = useFormik({
        initialValues: {
            firstName: '',
            lastName: '',
            age: '',
            phone: '',
            email: ''
        },
        validationSchema: personValidationSchema,
        onSubmit: async (values, { resetForm }) => {
            const result = await fetch('/api/people', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(values)
            })
            if (result.ok) {
                resetForm()
                mutate([...people, await result.json()])
                toast({
                    title: 'Utworzono osobę.',
                    description: `${values.firstName} ${values.lastName}`,
                    status: 'success',
                    isClosable: true,
                })
            } else {
                toast({
                    title: 'Wystąpił problem podczas dodawania osoby.',
                    status: "error",
                    isClosable: true,
                })
            }
        }
    })

    const handleDelete = async id => {
        const result = await fetch(`/api/people/${id}`, {
            method: 'DELETE',
        })
        if (result.ok) {
            mutate(people.filter(person => person.id !== id))
            toast({
                title: 'Osoba została usunięta.',
                status: 'success',
                isClosable: true,
            })
        } else {
            toast({
                title: 'Wystąpił problem podczas usuwania osoby.',
                status: "error",
                isClosable: true,
            })
        }
    }

    const handleEdit = async values => {
        const result = await fetch(`/api/people/${values.id}`, {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(values)
        })
        if (result.ok) {
            mutate(people.map(person => {
                if (person.id === values.id) {
                    return values
                } else {
                    return person
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
            <Heading mb={"4"} as={"h1"}>{t('Osoby')} {isLoading && <Spinner />}</Heading>
            {!isLoading && <TableContainer height={"100%"}>
                <Table size={"lg"}>
                    <Thead>
                        <Tr>
                            <Th>#</Th>
                            <Th>{t('Imię')}</Th>
                            <Th>{t('Nazwisko')}</Th>
                            <Th>{t('Wiek')}</Th>
                            <Th>{t('Numer telefonu')}</Th>
                            <Th>{t('E-mail')}</Th>
                            <Th></Th>
                        </Tr>
                    </Thead>
                    <Tbody>
                        <FormikProvider value={formik}>
                            <Tr>
                                <Td>
                                    <form onSubmit={formik.handleSubmit} id="addPersonForm"></form>
                                </Td>
                                <Td>
                                    <FormControl field="firstName" form="addPersonForm" type="text" />
                                </Td>
                                <Td>
                                    <FormControl field="lastName" form="addPersonForm" type="text" />
                                </Td>
                                <Td>
                                    <FormControl field="age" form="addPersonForm" type="number" min={0} />
                                </Td>
                                <Td>
                                    <FormControl field="phone" form="addPersonForm" type="tel" />
                                </Td>
                                <Td>
                                    <FormControl field="email" form="addPersonForm" type="email" />
                                </Td>
                                <Td><IconButton isDisabled={formik.isSubmitting} type="submit" form="addPersonForm" colorScheme="green" icon={<AddIcon />} /></Td>
                            </Tr>
                        </FormikProvider>
                        {people.map(person => <PersonTableRow key={person.id} person={person} onDelete={handleDelete} onEdit={handleEdit} />)}
                    </Tbody>
                </Table>
            </TableContainer>}
        </Box>
    )
}