<script setup lang="ts">
import { reactive, ref } from 'vue'
import api from '@/api';
import { CarTypes, type Car } from '@/api/models/car';
import type { RentalResponse } from '@/api/models/rent';

const formData = reactive({
    ssn: '',
    carType: 'compact'
});

const rentalResponse = ref<RentalResponse>();

async function onSubmit(event: Event) {
    // Send formData request to backend
    rentalResponse.value = await api.rentCar({
        ssn: formData.ssn,
        carType: formData.carType === 'compact' ? CarTypes.Compact
            : formData.carType === 'suv' ? CarTypes.SUV
            : CarTypes.Truck
    })

    // Clear form if successful
    formData.ssn = '';
    formData.carType = 'compact';
}

function onReset() {
    rentalResponse.value = undefined;
    formData.ssn = '';
    formData.carType = 'compact';
}
</script>

<template>
<div class="rent">
    <form v-if="rentalResponse == null" @submit.prevent="onSubmit">
        <h1>Hyr en bil</h1>
        <p>Dags att hyra en bil av högsta kvalité?</p>
        <p>Inga problem! Fyll i formuläret nedan så hittar vi det perfekta fordonet åt dig.</p>
        <div>
            <label for="ssn">Ditt personnummer</label>
            <input type="text" id="ssn" name="name" required v-model="formData.ssn" />
        </div>

        <div>
            <label for="car-type">Typ av bil</label>
            <select id="car-type" name="car-type" v-model="formData.carType">
                <option value="compact">🚗 Kompakt</option>
                <option value="suv">🚙 Kombi</option>
                <option value="truck">🚚 Lastbil</option>
            </select>
        </div>

        <button type="submit">Skicka förfrågan</button>
    </form>
    <div v-else-if="rentalResponse.rental != null && rentalResponse.availableCar != null" class="success">
        <h1>Grattis!</h1>
        <p>Vi har valt ut den bäst matchande bilen åt dig. Du kommer nog känna dig riktigt nöjd!</p>
        <img v-if="rentalResponse.availableCar.carType === CarTypes.Compact" class="car-image" src="@/assets/car-small.png" alt="Car image" />
        <img v-if="rentalResponse.availableCar.carType === CarTypes.SUV" class="car-image" src="@/assets/car-kombi.png" alt="Car image" />
        <img v-if="rentalResponse.availableCar.carType === CarTypes.Truck" class="car-image" src="@/assets/car-truck.png" alt="Car image" />
        <p>Du har fått tillgång till {{ rentalResponse.availableCar.registrationNumber }} som enbart gått {{ rentalResponse.availableCar.totalKilometers.toLocaleString() }} km hittills. En riktig lyxbil med andra ord!</p>
        <p>Bilen kommer skickas över till den plats du står på med drönare inom de närmaste 15-20 minuterna.</p>
        <p>Bokningsnummer: {{ rentalResponse.rental.bookingNumber }}</p>
        <p>Notera att du kan behöva ha bilens registreringsnummer, bokningsnumret eller ditt personnummer för att kunna lämna tillbaka bilen digitalt.</p>
    </div>
    <div v-else class="failure">
        <h2>Tyvärr kunde vi inte hitta en tillgänglig bil till dig</h2>
        <p>Du kanske kan försöka igen med en annan typ av bil?</p>
        <img class="car-image" src="@/assets/car-unavailable.png" alt="Car image" />
        <button @click="onReset">Prova igen</button>
    </div>
</div>
</template>

<style scoped>
.rent {
    .car-image {
        width: 30rem;
        max-width: 100%;
    }

    form {
        display: flex;
        flex-direction: column;
        gap: 1rem;
        margin: 2rem auto;
        max-width: 400px;
        
        > div {
            display: flex;
            flex-direction: column;
            gap: 0.5rem;
        }
    }

    .success {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 1rem;
    }

    .failure {
        display: flex;
        flex-direction: column;
        align-items: center;
        gap: 1rem;

        > .car-image {
            width: 15rem;
        }
    }
}
</style>