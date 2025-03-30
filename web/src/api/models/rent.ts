import type { Car, CarTypes } from "./car"

export interface RentRequest {
    ssn: string,
    carType: CarTypes
}

export interface RentalResponse {
    availableCar: Car | undefined
    rental: Rental | undefined
}

export interface Rental {
    id: string
    bookingNumber: string
    ssn: string
    carId: string
    startDate: string
    returnedDate: string | undefined
    totalKilometersBeforeRent: number
    totalKilometersAfterReturn: number | undefined
    totalKilometersDriven: number
    price: number | undefined
}

export interface ReturnRequest {
    number: string
    totalKilometers: number
}

export interface ReturnResponse {
    success: boolean
    message: string | undefined
    rental: Rental | undefined
    car: Car | undefined
}