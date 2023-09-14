module.exports = {
    lexers: {
        js: [{
            lexer: 'JsxLexer'
        }]
    },
    input: ['src/**/*.js'],
    output: 'public/locales/$LOCALE/$NAMESPACE.json',
    locales: ['pl', 'en'],
    keySeparator: false,
    defaultValue: (locale, namespace, key, value) => key
}