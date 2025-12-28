<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useAuthStore } from '@/stores';

const authStore = useAuthStore();
const loading = ref(true);

onMounted(async () => {
	try {
		await authStore.fetchUserInfo();
	} catch (error) {
		console.error('获取用户信息失败:', error);
	} finally {
		loading.value = false;
	}
});
</script>

<template>
	<div class="profile-page">
		<div v-if="loading" class="loading">加载中...</div>

		<div v-else-if="authStore.user" class="profile-content">
			<h1>个人资料</h1>

			<div class="profile-card">
				<h2>{{ authStore.user.username }}</h2>
				<p><strong>邮箱:</strong> {{ authStore.user.email }}</p>
				<p><strong>注册时间:</strong> {{ authStore.user.createdAt }}</p>
			</div>

			<div class="profile-actions">
				<button class="btn-edit">编辑资料</button>
				<button class="btn-logout" @click="authStore.logout()">退出登录</button>
			</div>
		</div>

		<div v-else class="not-logged-in">
			<h2>未登录</h2>
			<p>请先登录以查看个人资料。</p>
			<router-link to="/login" class="login-link">前往登录</router-link>
		</div>
	</div>
</template>
