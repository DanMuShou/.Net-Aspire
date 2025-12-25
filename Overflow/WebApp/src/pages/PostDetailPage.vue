<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { postApi, type PostQuestion } from '@/api/modules/post'

const route = useRoute()
const post = ref<PostQuestion | null>(null)
const loading = ref(true)

onMounted(async () => {
  try {
    const id = Number(route.params.id)
    post.value = await postApi.getPostById(id)
  } catch (error) {
    console.error('获取帖子详情失败:', error)
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div class="post-detail-page">
    <div v-if="loading" class="loading">
      加载中...
    </div>
    
    <div v-else-if="post" class="post-content">
      <h1>{{ post.title }}</h1>
      <div class="post-meta">
        <span>作者: {{ post.author }}</span>
        <span>时间: {{ post.createdAt }}</span>
        <span>回答数: {{ post.answerCount }}</span>
        <span>浏览数: {{ post.viewCount }}</span>
      </div>
      <div class="content">
        <p>{{ post.content }}</p>
      </div>
    </div>
    
    <div v-else class="not-found">
      <h2>帖子不存在</h2>
      <p>您要查找的帖子可能已被删除或不存在。</p>
    </div>
  </div>
</template>

<style scoped>
.post-detail-page {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.loading {
  text-align: center;
  padding: 2rem;
}

.post-content h1 {
  margin-top: 0;
  color: #333;
}

.post-meta {
  display: flex;
  gap: 1.5rem;
  margin: 1rem 0;
  font-size: 0.875rem;
  color: #666;
}

.content {
  margin-top: 2rem;
  line-height: 1.6;
}

.not-found {
  text-align: center;
  padding: 2rem;
}
</style>