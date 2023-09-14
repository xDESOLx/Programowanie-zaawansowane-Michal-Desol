//https://www.abeautifulsite.net/posts/getting-localized-month-and-day-names-in-the-browser/

export default function getDayNames(locale, format) {
    const formatter = new Intl.DateTimeFormat(locale, { weekday: format, timeZone: 'UTC' });
    const days = [1, 2, 3, 4, 5, 6, 7].map(day => {
        const dd = day < 10 ? `0${day}` : day;
        return new Date(`2017-01-${dd}T00:00:00+00:00`);
    });
    return days.map(date => formatter.format(date));
}