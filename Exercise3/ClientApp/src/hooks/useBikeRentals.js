import useSWR from 'swr'
import { fetcher } from '../lib/fetcher'

export default function useBikeRentals() {
    const { data, error, isLoading, mutate } = useSWR('/api/bikeRentals', fetcher)

    return {
        bikeRentals: data,
        isLoading,
        isError: error,
        mutate
    }
}