<script setup lang="ts">
import TheWelcome from '../components/TheWelcome.vue'
</script>

<template>
    <div class="home">
        <h1>Välkommen</h1>
        <p>Välj vad du vill göra</p>
    
        <nav>
            <RouterLink to="/rent">Hyr en bil</RouterLink>
            <RouterLink to="/return">Lämna tillbaka en bil</RouterLink>
        </nav>
    </div>
</template>