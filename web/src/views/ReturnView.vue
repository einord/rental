<script setup lang="ts">
import api from '@/api';
import type { ReturnRequest, ReturnResponse } from '@/api/models/rent';
import { reactive, ref } from 'vue'

const formData = reactive<ReturnRequest>({
    number: '',
    totalKilometers: 0
});

const result = ref<ReturnResponse>();

async function onSubmit(event: Event) {
    result.value = await api.returnCar(formData);

    if (result.value.success) {
        formData.number = '';
        formData.totalKilometers = 0;
    }
}
</script>

<template>
<div class="return">
    <div v-if="result == null || result.success === false">
        <h1>Returnera bil</h1>
        <div class="description">
            <img alt="broken car" src="@/assets/car-broken.png" />
            <div class="text">
                <p>Har du kört färdigt eller tröttnat längst vägen?</p>
                <p>Inga problem! Fyll i dina uppgifter nedan för att återlämna bilen. Så fort formuläret är korrekt ifyllt kommer bilen evaporeras och återskapas i vårt laboratorium.</p>
            </div>
        </div>

        <form @submit.prevent="onSubmit">
            <div>
                <label for="ssn">Ditt personnummer, bilens registreringsnummer eller bokningsnumret</label>
                <input type="text" id="number" name="number" required v-model="formData.number" />
            </div>
            <div>
                <label for="ssn">Aktuell mätarställning på bilen (km)</label>
                <input type="number" id="totalKilometers" name="totalKilometers" required v-model="formData.totalKilometers" />
            </div>

            <button type="submit">Lämna tillbaka bilen</button>
        </form>
    </div>
    <div class="success" v-else>
        <h1>Tack!</h1>
        <p>Du har nu registrerat en återlämning av {{ result.car?.registrationNumber }}.</p>
        <p>Du har åkt totalt {{ result.rental?.totalKilometersDriven.toLocaleString() }} km, vilket resulterar i ett pris på {{ result.rental?.price?.toLocaleString() ?? 0 }} kr</p>
        <p>Bilen kommer strax evaporeras av sig själv. Var god kontrollera att du inte lämnat kvar några värdesaker i bilen då dessa kommer desintegreras.</p>
    </div>
</div>
</template>

<style scoped>
.return {
    .description {
        display: flex;
        align-items: center;
        gap: 1rem;

        > img {
            width: 125px;
            aspect-ratio: 1/1;
        }

        > .text {
            flex: 1 1;
        }
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
}
</style>