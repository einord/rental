type httpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE';

class Api {
    baseUrl: string;

    constructor() {
        this.baseUrl = 'https://api.example.com';
    }

    /** Rents a car. */
    async rentCar() {
        return this.postData<string>('POST', 'rent', null);
    }

    /** Returns a rented car. */
    async returnCar() {
        return this.postData<string>('POST', 'return', null);
    }
    
    /**
     * Fetch data from the API.
     */
    private async postData<TResult>(method: httpMethod, endpoint: string, data: any) : Promise<TResult | void> {
        const response = await fetch(`${this.baseUrl}/${endpoint}`, {
            method,
            headers: {
                'Content-Type': 'application/json',
            },
            body: data == null ? undefined : JSON.stringify(data),
        });

        if (!response.ok) {
            throw new Error('Could not retrieve data from the API.');
        }

        // If response does not have a body, return void
        if (response.status === 204) {
            return;
        }

        // Return the response as JSON
        return response.json();
    }
}