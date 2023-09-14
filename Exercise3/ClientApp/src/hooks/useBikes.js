import useSWR from 'swr'
import { fetcher } from '../lib/fetcher'

export default function useBikes() {
    const { data, error, isLoading, mutate } = useSWR('/api/bikes', fetcher)

    return {
        bikes: data,
        isLoading,
        isError: error,
        mutate
    }
}