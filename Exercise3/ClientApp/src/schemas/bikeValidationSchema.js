import * as Yup from 'yup'
import { bikeTypes } from '../lib/bikeTypes'
import i18n from "../i18n"

const { t } = i18n

export const bikeValidationSchema = Yup.object({
    type: Yup.string().required(t('Typ roweru jest wymagany')).oneOf(Object.keys(bikeTypes), t('Nieprawidłowy typ roweru')),
    frameSize: Yup.number().typeError(t('Podano nieprawidłowy rozmiar ramy')).min(0, t('Rozmiar ramy nie może być ujemny')).max(99.99, t('Rozmiar ramy musi być mniejszy od 100')),
    wheelSize: Yup.number().typeError(t('Podano nieprawidłowy rozmiar kół')).min(0, t('Rozmiar kół nie może być ujemny')).max(99.99, t('Rozmiar kół musi być mniejszy od 100')),
    color: Yup.string().required(t('Kolor jest wymagany'))
})