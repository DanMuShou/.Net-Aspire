<
<template>
	<v-navigation-drawer v-model="drawer" :rail="rail" permanent @click="rail = false">
		<v-list>
			<v-list-item
				:prepend-icon="userIcon"
				:title="userName"
				:subtitle="userEmail"
				@click="handleUserClick"
			>
				<template v-slot:append>
					<v-btn icon="mdi-chevron-left" variant="text" @click.stop="rail = !rail"></v-btn>
				</template>
			</v-list-item>
		</v-list>

		<v-divider></v-divider>

		<v-list density="compact" nav>
			<v-list-item prepend-icon="mdi-home-city" title="Home" value="home"></v-list-item>
			<v-list-item prepend-icon="mdi-account" title="My Account" value="account"></v-list-item>
			<v-list-item
				prepend-icon="mdi-account-group-outline"
				title="Users"
				value="users"
			></v-list-item>
		</v-list>
	</v-navigation-drawer>
</template>
<script setup>
import { useAuthStore } from '@/stores';

const drawer = ref(true);
const rail = ref(true);

const authStore = useAuthStore();

const userIcon = computed(() => {
	if (authStore.user) {
		return 'mdi-account';
	} else {
		return 'mdi-account-off';
	}
});

const userName = computed(() => {
	if (authStore.user) {
		return authStore.user.username;
	} else {
		return '未登录';
	}
});

const userEmail = computed(() => {
	if (authStore.user) {
		return authStore.user.email;
	} else {
		return '';
	}
});

const handleUserClick = () => {
	if (!authStore.user) {
		console.log('未登录');
	} else {
		console.log('已登录');
	}
};
</script>
