import { useState } from "react"
import useBikes from "../hooks/useBikes"
import usePeople from "../hooks/usePeople"
import { FormikProvider, useFormik } from "formik"
import ActionsTableColumn from "./ActionsTableColumn"
import { bikeRentalValidationSchema } from "../schemas/bikeRentalValidationSchema"
import { Skeleton, Td, Tr } from "@chakra-ui/react"
import { bikeTypes } from "../lib/bikeTypes"
import FormControl from "./FormControl"

export default function BikeRentalTableRow({ bikeRental, onDelete, onEdit }) {
    const [mode, setMode] = useState('normal')
    const [disabled, setDisabled] = useState(false)

    const { people, isLoading: isLoadingPeople } = usePeople()
    const { bikes, isLoading: isLoadingBikes } = useBikes()

    const person = people?.find(p => p.id === parseInt(bikeRental.personId))
    const bike = bikes?.find(b => b.id === parseInt(bikeRental.bikeId))

    const formik = useFormik({
        initialValues: bikeRental,
        enableReinitialize: true,
        validationSchema: bikeRentalValidationSchema,
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
            onDelete(bikeRental.id)
        }}
        onConfirmEdit={formik.submitForm}
    />

    if (mode === 'editing') {
        return (
            <FormikProvider value={formik}>
                <Tr>
                    <Td>
                        <form onSubmit={formik.handleSubmit} id={`editBikeRentalForm-${bikeRental.id}`}></form>
                        {bikeRental.id}
                    </Td>
                    <Td>
                        <Skeleton isLoaded={!isLoadingPeople}>
                            <FormControl field="personId" form={`editBikeRentalForm-${bikeRental.id}`} type="select" placeholder="Wybierz osobę..." options={people?.map(person => ({
                                label: `${person.firstName} ${person.lastName}`,
                                value: person.id
                            }))} />
                        </Skeleton>
                    </Td>
                    <Td>
                        <Skeleton isLoaded={!isLoadingBikes}>
                            <FormControl field="bikeId" form={`editBikeRentalForm-${bikeRental.id}`} type="select" placeholder="Wybierz rower..." options={bikes?.map(bike => ({
                                label: `${bikeTypes[bike.type]} ${bike.color}`,
                                value: bike.id
                            }))} />
                        </Skeleton>
                    </Td>
                    <Td>
                        <FormControl field="rentalDate" form={`editBikeRentalForm-${bikeRental.id}`} type="date" />
                    </Td>
                    <Actions />
                </Tr>
            </FormikProvider>
        )
    }

    return (
        <Tr>
            <Td>{bikeRental.id}</Td>
            <Td>
                <Skeleton isLoaded={!isLoadingPeople}>
                    {person?.firstName ?? <>&nbsp;</>} {person?.lastName ?? <>&nbsp;</>}
                </Skeleton>
            </Td>
            <Td>
                <Skeleton isLoaded={!isLoadingBikes}>
                    {bikeTypes[bike?.type] ?? <>&nbsp;</>} {bike?.color ?? <>&nbsp;</>}
                </Skeleton>
            </Td>
            <Td>{bikeRental.rentalDate}</Td>
            <Actions />
        </Tr>
    )
}