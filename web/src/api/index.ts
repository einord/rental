import type { RentalResponse, RentRequest, ReturnRequest, ReturnResponse } from "./models/rent"

type httpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE'

class Api {
    baseUrl: string

    constructor() {
        this.baseUrl = import.meta.env.VITE_BACKEND_URL
        console.log(this.baseUrl)
    }

    /** Request to backend to rent a car. */
    async rentCar(request: RentRequest) {
        return this.postData<RentalResponse>('POST', 'rent', request)
    }

    /** Request to backend to return a rented car. */
    async returnCar(request: ReturnRequest) {
        return this.postData<ReturnResponse>('POST', 'return', request)
    }
    
    /**
     * Fetch data from the API.
     */
    private async postData<TResult>(method: httpMethod, endpoint: string, data: any) : Promise<TResult> {
        const response = await fetch(`${this.baseUrl}/${endpoint}`, {
            method,
            headers: {
                'Content-Type': 'application/json',
            },
            body: data == null ? undefined : JSON.stringify(data),
        })

        if (!response.ok) {
            throw new Error('Could not retrieve data from the API.')
        }

        // If response does not have a body, return nothing (void)
        if (response.status === 204) {
            return {} as TResult;
        }

        // Return the response as JSON
        return response.json()
    }
}

const api = new Api()
export default api