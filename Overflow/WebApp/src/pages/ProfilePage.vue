<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useAuthStore } from '@/stores'

const authStore = useAuthStore()
const loading = ref(true)

onMounted(async () => {
  try {
    await authStore.fetchUserInfo()
  } catch (error) {
    console.error('获取用户信息失败:', error)
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <div class="profile-page">
    <div v-if="loading" class="loading">
      加载中...
    </div>
    
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

<style scoped>
.profile-page {
  padding: 2rem;
  max-width: 800px;
  margin: 0 auto;
}

.loading {
  text-align: center;
  padding: 2rem;
}

.profile-card {
  background: #fff;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 2rem;
  margin-bottom: 2rem;
  box-shadow: 0 2px 4px rgba(0,0,0,0.05);
}

.profile-actions {
  display: flex;
  gap: 1rem;
}

.btn-edit, .btn-logout {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1rem;
}

.btn-edit {
  background-color: #3498db;
  color: white;
}

.btn-logout {
  background-color: #e74c3c;
  color: white;
}

.not-logged-in {
  text-align: center;
  padding: 2rem;
}

.login-link {
  display: inline-block;
  margin-top: 1rem;
  padding: 0.5rem 1rem;
  background-color: #3498db;
  color: white;
  text-decoration: none;
  border-radius: 4px;
}
</style>