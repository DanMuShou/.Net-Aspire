<template>
	<v-btn size="x-small" variant="plain" :icon="icon" @click="toggleTheme"></v-btn>
</template>
<script setup>
import { computed } from 'vue';
import { useTheme } from 'vuetify';

const theme = useTheme();
const toggleTheme = () => {
	theme.toggle();
};
const icon = computed(() => {
	return theme.current.value.dark ? 'mdi-brightness-4' : 'mdi-brightness-6';
});
</script>
