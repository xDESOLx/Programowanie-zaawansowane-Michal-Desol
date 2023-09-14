import { Td, Tr } from "@chakra-ui/react";
import ActionsTableColumn from "./ActionsTableColumn";
import { useState } from "react";
import { FormikProvider, useFormik } from "formik";
import { personValidationSchema } from "../schemas/personValidationSchema";
import FormControl from "./FormControl";

export default function PersonTableRow({ person, onDelete, onEdit }) {
    const [mode, setMode] = useState('normal')
    const [disabled, setDisabled] = useState(false)

    const formik = useFormik({
        initialValues: person,
        enableReinitialize: true,
        validationSchema: personValidationSchema,
        onSubmit: async values => {
            setDisabled(true)
            await onEdit(values)
            setMode('normal')
            setDisabled(false)
        }
    })

    const Actions = () => <ActionsTableColumn
        mode={mode}
        disabled={disabled}
        onEdit={() => {
            formik.resetForm()
            setMode('editing')
        }}
        onDelete={() => setMode('deleting')}
        onCancelEdit={() => setMode('normal')}
        onCancelDelete={() => setMode('normal')}
        onConfirmDelete={() => {
            setDisabled(true)
            onDelete(person.id)
        }}
        onConfirmEdit={formik.submitForm}
    />

    if (mode === 'editing') {
        return (
            <FormikProvider value={formik}>
                <Tr>
                    <Td>
                        <form onSubmit={formik.handleSubmit} id={`editPersonForm-${person.id}`}></form>
                        {person.id}
                    </Td>
                    <Td>
                        <FormControl field="firstName" form={`editPersonForm-${person.id}`} type="text" />
                    </Td>
                    <Td>
                        <FormControl field="lastName" form={`editPersonForm-${person.id}`} type="text" />
                    </Td>
                    <Td>
                        <FormControl field="age" form={`editPersonForm-${person.id}`} type="number" min={0} />
                    </Td>
                    <Td>
                        <FormControl field="phone" form={`editPersonForm-${person.id}`} type="tel" />
                    </Td>
                    <Td>
                        <FormControl field="email" form={`editPersonForm-${person.id}`} type="email" />
                    </Td>
                    <Actions />
                </Tr>
            </FormikProvider>
        )
    }

    return (
        <Tr>
            <Td>{person.id}</Td>
            <Td>{person.firstName}</Td>
            <Td>{person.lastName}</Td>
            <Td>{person.age}</Td>
            <Td>{person.phone}</Td>
            <Td>{person.email}</Td>
            <Actions />
        </Tr>
    )
}