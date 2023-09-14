import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import Backend from 'i18next-http-backend';
import detector from "i18next-browser-languagedetector";

await i18n
    .use(Backend)
    .use(detector)
    .use(initReactI18next)
    .init({
        supportedLngs: ['pl', 'en'],
        fallbackLng: 'pl',
        keySeparator: false
    });


export default i18n;