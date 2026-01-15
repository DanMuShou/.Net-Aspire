<template>
  <v-app>
    <router-view />
    <GlobalAlert />
  </v-app>
</template>

<script setup>
import GlobalAlert from '@/components/global/GlobalAlert.vue'
</script>
