<template>
  <v-menu offset-y>
    <template v-slot:activator="{ props }">
      <v-btn v-bind="props" icon>
        <v-avatar size="40" color="teal" class="text-white">
          {{ getUserInitials }}
        </v-avatar>
      </v-btn>
    </template>
    <v-list>
      <v-list-item>
        <v-list-item-title>{{ authStore.user?.name || authStore.user?.username }}</v-list-item-title>
        <v-list-item-subtitle>{{ authStore.user?.email }}</v-list-item-subtitle>
      </v-list-item>
      <v-divider></v-divider>
      <v-list-item @click="logout">
        <v-list-item-title>退出登录</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-menu>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '../../stores'

const router = useRouter()
const authStore = useAuthStore()

const getUserInitials = computed(() => {
  if (authStore.user?.name) {
    return authStore.user.name.charAt(0).toUpperCase()
  } else if (authStore.user?.username) {
    return authStore.user.username.charAt(0).toUpperCase()
  }
  return '?'
})

const logout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<style></style>