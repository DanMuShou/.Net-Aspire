<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { postApi, type PostQuestion } from '@/api/modules/post';

const posts = ref<PostQuestion[]>([]);
const loading = ref(true);

onMounted(async () => {
	try {
		posts.value = await postApi.getPosts();
	} catch (error) {
		console.error('获取帖子列表失败:', error);
	} finally {
		loading.value = false;
	}
});
</script>

<template>
	<div class="post-list-page">
		<h1>帖子列表</h1>

		<div v-if="loading" class="loading">加载中...</div>

		<div v-else class="post-list">
			<div v-for="post in posts" :key="post.id" class="post-item">
				<h3>{{ post.title }}</h3>
				<p>{{ post.content.substring(0, 100) }}...</p>
				<div class="post-meta">
					<span>作者: {{ post.author }}</span>
					<span>回答数: {{ post.answerCount }}</span>
					<span>浏览数: {{ post.viewCount }}</span>
					<span>时间: {{ post.createdAt }}</span>
				</div>
			</div>
		</div>
	</div>
</template>
