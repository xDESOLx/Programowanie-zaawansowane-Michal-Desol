import * as Yup from 'yup'
import i18n from "../i18n"

const { t } = i18n

export const bookRentalValidationSchema = Yup.object({
    personId: Yup.number().integer().required(t('Osoba jest wymagana')),
    bookId: Yup.number().integer().required(t('Książka jest wymagana')),
    rentalDate: Yup.date().typeError(t('Wprowadzono nieprawidłową datę')).required(t('Data wypożyczenia jest wymagana'))
})