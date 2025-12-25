<template>
  <v-layout>
    <v-app-bar
      color=".bg-blue"
      density="compact"
      scroll-behavior="hide"
      scroll-threshold="0"
    >
      <v-app-bar-title>
        <v-icon class="mx-2" icon="mdi-dot-net" size="x-large" />
        <span class="text-h6">Overflow</span>
      </v-app-bar-title>
      <SearchBar />
      <v-spacer />
      <ThemeButton class="ml-1" />
      <LoginButton class="mx-1" />
      <RegisterButton class="mx-1" />
    </v-app-bar>

    <v-navigation-drawer
      v-model="drawer"
      permanent
      :rail="rail"
      @click="rail = false"
    >
      <v-list>
        <v-list-item
          :prepend-icon="userIcon"
          :subtitle="userEmail"
          :title="userName"
          @click="handleUserClick"
        >
          <template #append>
            <v-btn
              icon="mdi-chevron-left"
              variant="text"
              @click.stop="changeRail"
            />
          </template>
        </v-list-item>
      </v-list>

      <v-divider />

      <slot name="sideNav" />
    </v-navigation-drawer>

    <v-main>
      <slot name="default" />
    </v-main>
  </v-layout>
</template>

<script setup lang="ts">
import { useAuthStore } from '@/stores'

const drawer = ref(true)
const rail = ref(true)

const authStore = useAuthStore()

const userIcon = computed(() => {
  return authStore.user ? 'mdi-account' : 'mdi-account-off'
})

const userName = computed(() => {
  return authStore.user ? authStore.user.username : '未登录'
})

const userEmail = computed(() => {
  return authStore.user ? authStore.user.email : ''
})

function changeRail(): void {
  rail.value = !rail.value
}

function handleUserClick(): void {
  if (authStore.user) {
    console.log('已登录')
  } else {
    console.log('未登录')
  }
}
</script>
