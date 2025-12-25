<script setup lang="ts">
import { onMounted } from 'vue'
import { useCommonStore } from '@/stores'

const commonStore = useCommonStore()

onMounted(() => {
  commonStore.showSnackbar('欢迎来到首页！', 'success')
})
</script>

<template>
  <div class="home-page">
    <h1>Overflow 论坛</h1>
    <p>这是一个技术问答社区</p>
    
    <div class="features">
      <h2>功能特性</h2>
      <ul>
        <li>提问与回答</li>
        <li>标签分类</li>
        <li>搜索功能</li>
        <li>用户社区</li>
      </ul>
    </div>
  </div>
</template>

<style scoped>
.home-page {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.features {
  margin-top: 2rem;
}

.features ul {
  list-style: none;
  padding: 0;
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 1rem;
}

.features li {
  background: #f5f5f5;
  padding: 1rem;
  border-radius: 4px;
  text-align: center;
}
</style>