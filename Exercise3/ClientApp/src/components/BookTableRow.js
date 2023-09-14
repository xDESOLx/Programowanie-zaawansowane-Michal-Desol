import { FormikProvider, useFormik } from "formik"
import { bookValidationSchema } from "../schemas/bookValidationSchema"
import ActionsTableColumn from "./ActionsTableColumn"
import { Td, Tr } from "@chakra-ui/react"
import FormControl from "./FormControl"
import { useState } from "react"

export default function BookTableRow({ book, onDelete, onEdit }) {
    const [mode, setMode] = useState('normal')
    const [disabled, setDisabled] = useState(false)

    const formik = useFormik({
        initialValues: book,
        enableReinitialize: true,
        validationSchema: bookValidationSchema,
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
            onDelete(book.id)
        }}
        onConfirmEdit={formik.submitForm}
    />

    if (mode === 'editing') {
        return (
            <FormikProvider value={formik}>
                <Tr>
                    <Td>
                        <form onSubmit={formik.handleSubmit} id={`editBookForm-${book.id}`}></form>
                        {book.id}
                    </Td>
                    <Td>
                        <FormControl field="title" form={`editBookForm-${book.id}`} type="text" />
                    </Td>
                    <Td>
                        <FormControl field="author" form={`editBookForm-${book.id}`} type="text" />
                    </Td>
                    <Td>
                        <FormControl field="genre" form={`editBookForm-${book.id}`} type="text" />
                    </Td>
                    <Td>
                        <FormControl field="isbn" form={`editBookForm-${book.id}`} type="text" />
                    </Td>
                    <Actions />
                </Tr>
            </FormikProvider>
        )
    }

    return (
        <Tr>
            <Td>{book.id}</Td>
            <Td>{book.title}</Td>
            <Td>{book.author}</Td>
            <Td>{book.genre}</Td>
            <Td>{book.isbn}</Td>
            <Actions />
        </Tr>
    )
}