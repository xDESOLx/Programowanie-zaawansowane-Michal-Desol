import * as Yup from 'yup'
import i18n from "../i18n"

const { t } = i18n

export const bikeRentalValidationSchema = Yup.object({
    personId: Yup.number().integer().required(t('Osoba jest wymagana')),
    bikeId: Yup.number().integer().required(t('Rower jest wymagany')),
    rentalDate: Yup.date().typeError(t('Wprowadzono nieprawidłową datę')).required(t('Data wypożyczenia jest wymagana'))
})