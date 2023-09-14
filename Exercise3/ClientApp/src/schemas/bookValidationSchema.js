import * as Yup from 'yup'
import i18n from "../i18n"

const { t } = i18n

export const bookValidationSchema = Yup.object({
    title: Yup.string().required(t('Tytuł jest wymagany')),
    author: Yup.string().required(t('Autor jest wymagany')),
    genre: Yup.string().required(t('Gatunek jest wymagany')),
    isbn: Yup.string().required(t('ISBN jest wymagany')),
})