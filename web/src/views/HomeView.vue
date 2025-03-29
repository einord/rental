<script setup lang="ts">
import TheWelcome from '../components/TheWelcome.vue'
import Quote from '@/components/Quote.vue'
</script>

<template>
<div class="home">
    <h1>Hyr personliga fordon med själ</h1>

    <quote by="Linda">Bilen startade efter bara tre försök! Ett riktigt kap om du frågar mig.</quote>
    <quote by="Benny">Den där konstiga lukten i bagaget? Charmigt! Tog oss hela vägen till Norrköping - och tillbaka (nästan)</quote>
    <quote by="Joakim">Jag älskar att varje repa har en historia.</quote>
    <quote by="Sara">Styrningen var lite… unik. Men man vänjer sig efter ett tag. Och radion funkade ibland!</quote>

</div>
</template>

<style scoped>
.home {
    text-align: center;
}
</style>