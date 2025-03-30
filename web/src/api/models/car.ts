export enum CarTypes {
    Compact,
    SUV,
    Truck
}

export interface Car
{
    id: string;
    registrationNumber: string;
    carType: CarTypes;
    totalKilometers: number;
}