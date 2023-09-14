import useSWR from 'swr'
import { fetcher } from '../lib/fetcher'

export default function useBooks() {
    const { data, error, isLoading, mutate } = useSWR('/api/books', fetcher)

    return {
        books: data,
        isLoading,
        isError: error,
        mutate
    }
}