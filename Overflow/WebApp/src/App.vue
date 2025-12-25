<script setup lang="ts">
import { RouterView } from 'vue-router'
import { useAuthStore } from './stores'
import { onMounted } from 'vue'
import MainLayout from './layouts/MainLayout.vue'

const authStore = useAuthStore()

onMounted(() => {
  // 尝试从本地存储恢复用户信息
  if (authStore.token) {
    authStore.fetchUserInfo().catch(err => {
      console.error('恢复用户信息失败:', err)
    })
  }
})
</script>

<template>
  <MainLayout>
    <RouterView />
  </MainLayout>
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

.auth-section {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.logout-btn {
  padding: 0.25rem 0.5rem;
  background-color: #e74c3c;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

main {
  min-height: calc(100vh - 120px);
}

footer {
  padding: 1rem;
  text-align: center;
  background-color: #f5f5f5;
  color: #666;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;
    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>