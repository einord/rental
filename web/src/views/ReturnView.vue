<script setup lang="ts">
import { ref } from 'vue'
import TheWelcome from '../components/TheWelcome.vue'

const number = ref('');

function onSubmit(event: Event) {
    // TODO: Send formData to backend
    alert('Tack!');
    number.value = '';
}
</script>

<template>
<div class="return">
    <h1>Returnera bil</h1>
    <div class="description">
        <img alt="broken car" src="@/assets/car-broken.png" />
        <div class="text">
            <p>Har du kört färdigt eller tröttnat längst vägen?</p>
            <p>Inga problem! Fyll i formuläret nedan för att återlämna bilen. Så fort formuläret är ifyllt kommer bilen evaporeras och återskapas i vårt laboratorium.</p>
        </div>
    </div>

    <form @submit.prevent="onSubmit">
        <div>
            <label for="ssn">Ditt personnummer / bilens registreringsnummer</label>
            <input type="text" id="number" name="number" required v-model="number" />
        </div>

        <button type="submit">Lämna tillbaka bilen</button>
    </form>
</div>
</template>

<style scoped>
.return {
    > .description {
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
}
</style>