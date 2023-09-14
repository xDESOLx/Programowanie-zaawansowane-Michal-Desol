import * as Yup from 'yup'
import i18n from "../i18n"

const { t } = i18n

export const personValidationSchema = Yup.object({
    firstName: Yup.string().required(t('Imię jest wymagane')),
    lastName: Yup.string().required(t('Nazwisko jest wymagane')),
    age: Yup.number().typeError(t('Podano nieprawidłowy wiek')).min(0, t('Wiek nie może być ujemny')).integer(t('Wiek musi być liczbą całkowitą')).required(t('Wiek jest wymagany')),
    phone: Yup.string().matches(/^\d{9}$/g, t('Nieprawidłowy numer telefonu')).required(t('Numer telefonu jest wymagany')),
    email: Yup.string().email(t('Nieprawidłowy adres e-mail')).required(t('Adres e-mail jest wymagany'))
})