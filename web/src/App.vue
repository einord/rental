<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
</script>

<template>
    <header>
        <RouterLink to="/" class="logo-wrapper">
            <img alt="Car rental logo" class="logo" src="@/assets/logo.png" width="125" height="125" />
            <div class="title">
                <h1>Quality Car Biluthyrning</h1>
                <p>Biluthyrning du kan lita på</p>
            </div>
        </RouterLink>

        <nav>
            <RouterLink to="/rent">Hyr en bil</RouterLink>
            <RouterLink to="/return">Returnera</RouterLink>
        </nav>
    </header>

    <main>
        <RouterView />
    </main>
</template>

<style scoped>
header {
    line-height: 1.25;
    max-height: 100vh;
    padding-bottom: 2rem;
    border-bottom: 1px solid var(--vt-c-text-dark-2);
    margin-bottom: 2rem;
}

.logo-wrapper {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: row;
    gap: 1rem;
    width: 100%;
    text-decoration: unset;
    background-color: unset;
    color: unset;

    > .logo {
        display: block;
    }

    > .title {
        > p {
            font-weight: 500;
            letter-spacing: 0.3em;
        }
    }
}

nav {
    width: 100%;
    font-size: 12px;
    text-align: center;
    font-weight: bold;
    display: flex;
    justify-content: center;
    gap: 1rem;

    a {
        background-color: var(--vt-c-indigo-semi-transparent);
        padding: 0.5em 1em;
        height: 2rem;
        display: flex;
        align-items: center;
        border-radius: 1em;
        color: white;
        font-weight: bold;
    }

    a.router-link-exact-active {
        background-color: var(--vt-c-indigo);
    }
}

nav a:hover {
    background-color: var(--vt-c-indigo);
}

main {

}

@media (min-width: 1024px) {
    header {
        display: flex;
        flex-direction: column;
        place-items: center;
        justify-content: center;
        padding-right: calc(var(--section-gap) / 2);
        
        padding-bottom: 0;
        border-bottom: none;
        margin-bottom: 0;

        padding-right: 2rem;
        border-right: 1px solid var(--vt-c-text-dark-2);
        margin-right: 2rem;

        .logo-wrapper {
            justify-content: start;

            > .title {
                flex: 1 1;
            }
        }
    }

    header .wrapper {
        display: flex;
        place-items: flex-start;
        flex-wrap: wrap;
    }

    nav {
        font-size: 1rem;

        a {
            height: 3rem;
        }
    }
}
</style>
