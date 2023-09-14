import { FormikProvider, useFormik } from "formik"
import { useState } from "react"
import { bikeValidationSchema } from "../schemas/bikeValidationSchema"
import ActionsTableColumn from "./ActionsTableColumn"
import { Td, Tr } from "@chakra-ui/react"
import FormControl from "./FormControl"
import { bikeTypes } from "../lib/bikeTypes"

export default function BikeTableRow({ bike, onDelete, onEdit }) {
    const [mode, setMode] = useState('normal')
    const [disabled, setDisabled] = useState(false)

    const formik = useFormik({
        initialValues: bike,
        enableReinitialize: true,
        validationSchema: bikeValidationSchema,
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
            onDelete(bike.id)
        }}
        onConfirmEdit={formik.submitForm}
    />

    if (mode === 'editing') {
        return (
            <FormikProvider value={formik}>
                <Tr>
                    <Td>
                        <form onSubmit={formik.handleSubmit} id={`editBikeForm-${bike.id}`}></form>
                        {bike.id}
                    </Td>
                    <Td>
                        <FormControl
                            field="type"
                            form={`editBikeForm-${bike.id}`}
                            type="select"
                            placeholder="Wybierz typ..."
                            options={Object.entries(bikeTypes).map(([value, label]) => ({ label, value }))}
                        />
                    </Td>
                    <Td>
                        <FormControl field="frameSize" form={`editBikeForm-${bike.id}`} type="number" min={0} max={99.99} />
                    </Td>
                    <Td>
                        <FormControl field="wheelSize" form={`editBikeForm-${bike.id}`} type="number" min={0} max={99.99} />
                    </Td>
                    <Td>
                        <FormControl field="color" form={`editBikeForm-${bike.id}`} type="text" />
                    </Td>
                    <Actions />
                </Tr>
            </FormikProvider>
        )
    }

    return (
        <Tr>
            <Td>{bike.id}</Td>
            <Td>{bikeTypes[bike.type]}</Td>
            <Td>{bike.frameSize}</Td>
            <Td>{bike.wheelSize}</Td>
            <Td>{bike.color}</Td>
            <Actions />
        </Tr>
    )
}