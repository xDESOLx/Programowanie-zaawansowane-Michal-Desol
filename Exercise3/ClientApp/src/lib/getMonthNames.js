//https://www.abeautifulsite.net/posts/getting-localized-month-and-day-names-in-the-browser/

export default function getMonthNames(locale, format) {
    const formatter = new Intl.DateTimeFormat(locale, { month: format, timeZone: 'UTC' });
    const months = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12].map(month => {
        const mm = month < 10 ? `0${month}` : month;
        return new Date(`2017-${mm}-01T00:00:00+00:00`);
    });
    return months.map(date => formatter.format(date));
}