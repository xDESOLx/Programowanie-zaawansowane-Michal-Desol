import useSWR from 'swr'
import { fetcher } from '../lib/fetcher'

export default function useBookRentals() {
    const { data, error, isLoading, mutate } = useSWR('/api/bookRentals', fetcher)

    return {
        bookRentals: data,
        isLoading,
        isError: error,
        mutate
    }
}