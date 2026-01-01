<template>
	<v-app-bar scroll-behavior="hide" density="compact" scroll-threshold="0">
		<template v-slot:prepend>
			<v-icon class="mx-2" icon="mdi-dot-net" size="x-large"></v-icon>
			<span class="text-h6">Overflow</span>
			<v-app-bar-nav-icon></v-app-bar-nav-icon>
		</template>
		<v-app-bar-title text="标题"></v-app-bar-title>
		<SearchBar></SearchBar>

		<ThemeButton class="ml-1"></ThemeButton>
		<LoginButton class="mx-1"></LoginButton>
		<RegisterButton class="mx-1"></RegisterButton>
	</v-app-bar>
</template>
<script setup>
import SearchBar from './SearchBar.vue';
import ThemeButton from './ThemeButton.vue';
import LoginButton from './LoginButton.vue';
import RegisterButton from './RegisterButton.vue';
</script>
<style lang=""></style>
