<template>
	<v-layout>
		<v-app-bar
			color=".bg-blue"
			scroll-behavior="hide"
			density="compact"
			scroll-threshold="0">
			<v-app-bar-title>
				<v-icon class="mx-2" icon="mdi-dot-net" size="x-large"></v-icon>
				<span class="text-h6">Overflow</span>
			</v-app-bar-title>
			<SearchBar></SearchBar>
			<v-spacer></v-spacer>
			<ThemeButton class="ml-1"></ThemeButton>
			<LoginButton class="mx-1"></LoginButton>
			<RegisterButton class="mx-1"></RegisterButton>
		</v-app-bar>

		<v-navigation-drawer
			v-model="drawer"
			:rail="rail"
			permanent
			@click="rail = false">
			<v-list>
				<v-list-item
					:prepend-icon="userIcon"
					:title="userName"
					:subtitle="userEmail"
					@click="handleUserClick">
					<template v-slot:append>
						<v-btn
							icon="mdi-chevron-left"
							variant="text"
							@click.stop="changeRail"></v-btn>
					</template>
				</v-list-item>
			</v-list>

			<v-divider></v-divider>

			<slot name="sideNav"></slot>
		</v-navigation-drawer>

		<v-main>
			<slot name="default"></slot>
		</v-main>
	</v-layout>
</template>

<script setup>
import { useAuthStore } from '@/stores'

const drawer = ref(true)
const rail = ref(true)

const authStore = useAuthStore()

const userIcon = computed(() => {
	if (authStore.user) {
		return 'mdi-account'
	} else {
		return 'mdi-account-off'
	}
})

const userName = computed(() => {
	if (authStore.user) {
		return authStore.user.username
	} else {
		return '未登录'
	}
})

const userEmail = computed(() => {
	if (authStore.user) {
		return authStore.user.email
	} else {
		return ''
	}
})

const changeRail = () => {
	rail.value = !rail.value
}

const handleUserClick = () => {
	if (!authStore.user) {
		console.log('未登录')
	} else {
		console.log('已登录')
	}
}
</script>
