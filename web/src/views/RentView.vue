<script setup lang="ts">
import { reactive } from 'vue'
import TheWelcome from '../components/TheWelcome.vue'

const formData = reactive({
    ssn: '',
    carType: 'compact'
});

function onSubmit(event: Event) {
    // TODO: Send formData to backend
    alert('Formuläret har skickats!');
    formData.ssn = '';
    formData.carType = 'compact';
}
</script>

<template>
    <h1>Hyr en bil</h1>
    <p>Dags att hyra en bil av högsta kvalité?</p>
    <p>Inga problem! Fyll i formuläret nedan så hittar vi det perfekta fordonet åt dig.</p>

    <form @submit.prevent="onSubmit">
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
</template>

<style scoped>

</style>