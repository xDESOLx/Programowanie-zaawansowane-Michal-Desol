import useSWR from 'swr'
import { fetcher } from '../lib/fetcher'

export default function usePeople() {
    const { data, error, isLoading, mutate } = useSWR('/api/people', fetcher)

    return {
        people: data,
        isLoading,
        isError: error,
        mutate
    }
}