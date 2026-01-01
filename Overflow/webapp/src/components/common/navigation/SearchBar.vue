<template>
  <v-text-field
    :loading="loading"
    append-inner-icon="mdi-magnify"
    density="compact"
    label="搜索..."
    variant="solo"
    hide-details
    single-line
    @click:append-inner="onClick"></v-text-field>
</template>
<script setup>
import { ref } from 'vue';

const loading = ref(false);

function onClick() {
  loading.value = true;
  setTimeout(() => (loading.value = false), 2000);
  console.log('clicked');
}
</script>
