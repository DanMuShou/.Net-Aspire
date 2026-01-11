<template>
  <v-app>
    <WorkbenchLayout v-if="test">
      <router-view />
    </WorkbenchLayout>
    <FocusLayout v-else>
      <router-view />
    </FocusLayout>
    <GlobalAlert />
  </v-app>
</template>

<script setup>
import GlobalAlert from '@/components/global/GlobalAlert.vue'
import FocusLayout from './layouts/FocusLayout.vue'
import WorkbenchLayout from './layouts/WorkbenchLayout.vue'

const test = ref(false)
</script>
