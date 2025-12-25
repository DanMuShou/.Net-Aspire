<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { postApi, type PostQuestion } from '@/api/modules/post'

const posts = ref<PostQuestion[]>([])
const loading = ref(true)

onMounted(async () => {
  try {
    posts.value = await postApi.getPosts()
  } catch (error) {
    console.error('获取帖子列表失败:', error)
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div class="post-list-page">
    <h1>帖子列表</h1>
    
    <div v-if="loading" class="loading">
      加载中...
    </div>
    
    <div v-else class="post-list">
      <div 
        v-for="post in posts" 
        :key="post.id" 
        class="post-item"
      >
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

<style scoped>
.post-list-page {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.loading {
  text-align: center;
  padding: 2rem;
}

.post-item {
  background: #fff;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 1.5rem;
  margin-bottom: 1rem;
  box-shadow: 0 2px 4px rgba(0,0,0,0.05);
}

.post-item h3 {
  margin-top: 0;
  color: #333;
}

.post-meta {
  display: flex;
  gap: 1rem;
  margin-top: 1rem;
  font-size: 0.875rem;
  color: #666;
}

.post-meta span {
  display: inline-block;
}
</style>