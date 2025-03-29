<script setup lang="ts">
import { defineProps } from 'vue'

const props = defineProps<{
    by: string
}>()
</script>

<template>
<div class="quote">
    <p class="text">
        &quot;<slot></slot>&quot;
    </p>
    <p class="by">&dash;&nbsp;{{ props.by }}</p>
</div>
</template>

<style scoped>
.quote {
    margin: 0;
    padding: 1rem;
    font-size: 1rem;
    font-weight: 500;
    text-align: center;
    color: var(--color-text);

    > .text {
        font-size: 1.2rem;
        font-weight: 600;
        font-style: italic;
        text-wrap: balance;
        color: var(--color-heading);
    }
    
    > .by {
        font-size: 0.8rem;
        font-weight: 400;
        color: var(--color-text);
    }
}
</style>