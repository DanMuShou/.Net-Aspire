<template>
  <div class="main-layout">
    <header class="main-header">
      <div class="container">
        <h1 class="logo">Overflow</h1>
        <nav class="main-nav">
          <RouterLink to="/">首页</RouterLink>
          <RouterLink to="/posts">帖子</RouterLink>
          <RouterLink to="/about">关于</RouterLink>
        </nav>
        <div class="auth-section">
          <template v-if="$store?.auth?.isAuthenticated">
            <RouterLink to="/profile">个人资料</RouterLink>
            <button @click="$store?.auth?.logout()" class="logout-btn">退出</button>
          </template>
          <template v-else>
            <RouterLink to="/login">登录</RouterLink>
          </template>
        </div>
      </div>
    </header>

    <main class="main-content">
      <slot />
    </main>

    <footer class="main-footer">
      <div class="container">
        <p>&copy; 2025 Overflow. All rights reserved.</p>
      </div>
    </footer>
  </div>
</template>

<script setup lang="ts">
// 这里可以添加布局相关的逻辑
</script>

<style scoped>
.main-layout {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.main-header {
  background-color: #fff;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  padding: 1rem 0;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.logo {
  font-size: 1.5rem;
  font-weight: bold;
  color: #3498db;
  margin: 0;
}

.main-nav {
  display: flex;
  gap: 2rem;
}

.main-nav a {
  text-decoration: none;
  color: #333;
  font-weight: 500;
}

.main-nav a.router-link-active {
  color: #3498db;
}

.auth-section {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.logout-btn {
  padding: 0.25rem 0.5rem;
  background-color: #e74c3c;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.main-content {
  flex: 1;
  padding: 2rem 0;
}

.main-footer {
  background-color: #f5f5f5;
  padding: 1rem 0;
  margin-top: auto;
}

.main-footer p {
  margin: 0;
  text-align: center;
  color: #666;
}
</style>