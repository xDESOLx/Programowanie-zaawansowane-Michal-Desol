import { useState } from "react"
import usePeople from "../hooks/usePeople"
import useBooks from "../hooks/useBooks"
import { FormikProvider, useFormik } from "formik"
import ActionsTableColumn from "./ActionsTableColumn"
import { bookRentalValidationSchema } from "../schemas/bookRentalValidationSchema"
import { Skeleton, Td, Tr } from "@chakra-ui/react"
import FormControl from "./FormControl"

export default function BookRentalTableRow({ bookRental, onDelete, onEdit }) {
    const [mode, setMode] = useState('normal')
    const [disabled, setDisabled] = useState(false)

    const { people, isLoading: isLoadingPeople } = usePeople()
    const { books, isLoading: isLoadingBooks } = useBooks()

    const person = people?.find(p => p.id === parseInt(bookRental.personId))
    const book = books?.find(b => b.id === parseInt(bookRental.bookId))

    const formik = useFormik({
        initialValues: bookRental,
        enableReinitialize: true,
        validationSchema: bookRentalValidationSchema,
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
            onDelete(bookRental.id)
        }}
        onConfirmEdit={formik.submitForm}
    />

    if (mode === 'editing') {
        return (
            <FormikProvider value={formik}>
                <Tr>
                    <Td>
                        <form onSubmit={formik.handleSubmit} id={`editBookRentalForm-${bookRental.id}`}></form>
                        {bookRental.id}
                    </Td>
                    <Td>
                        <Skeleton isLoaded={!isLoadingPeople}>
                            <FormControl field="personId" form={`editBookRentalForm-${bookRental.id}`} type="select" placeholder="Wybierz osobę..." options={people?.map(person => ({
                                label: `${person.firstName} ${person.lastName}`,
                                value: person.id
                            }))} />
                        </Skeleton>
                    </Td>
                    <Td>
                        <Skeleton isLoaded={!isLoadingBooks}>
                            <FormControl field="bookId" form={`editBookRentalForm-${bookRental.id}`} type="select" placeholder="Wybierz książkę..." options={books?.map(book => ({
                                label: `${book.author} - ${book.title}`,
                                value: book.id
                            }))} />
                        </Skeleton>
                    </Td>
                    <Td>
                        <FormControl field="rentalDate" form={`editBookRentalForm-${bookRental.id}`} type="date" />
                    </Td>
                    <Actions />
                </Tr>
            </FormikProvider>
        )
    }

    return (
        <Tr>
            <Td>{bookRental.id}</Td>
            <Td>
                <Skeleton isLoaded={!isLoadingPeople}>
                    {person?.firstName ?? <>&nbsp;</>} {person?.lastName ?? <>&nbsp;</>}
                </Skeleton>
            </Td>
            <Td>
                <Skeleton isLoaded={!isLoadingBooks}>
                    {book?.author ?? <>&nbsp;</>} - {book?.title ?? <>&nbsp;</>}
                </Skeleton>
            </Td>
            <Td>{bookRental.rentalDate}</Td>
            <Actions />
        </Tr>
    )
}