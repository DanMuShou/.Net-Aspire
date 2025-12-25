<script setup lang="ts">
import { ref } from 'vue'

const teamMembers = ref([
  { name: '开发团队', role: '开发者' },
  { name: '设计团队', role: 'UI/UX设计师' },
  { name: '测试团队', role: 'QA工程师' },
])
</script>

<template>
  <div class="about-page">
    <h1>关于我们</h1>
    <p>Overflow是一个技术问答平台，致力于为开发者提供高质量的技术交流环境。</p>
    
    <section class="team">
      <h2>团队成员</h2>
      <div class="members">
        <div 
          v-for="(member, index) in teamMembers" 
          :key="index" 
          class="member-card"
        >
          <h3>{{ member.name }}</h3>
          <p>{{ member.role }}</p>
        </div>
      </div>
    </section>
    
    <section class="tech-stack">
      <h2>技术栈</h2>
      <ul>
        <li>前端: Vue 3, TypeScript, Vite</li>
        <li>状态管理: Pinia</li>
        <li>路由: Vue Router</li>
        <li>后端: .NET Aspire</li>
        <li>数据库: PostgreSQL</li>
        <li>搜索引擎: Typesense</li>
      </ul>
    </section>
  </div>
</template>

<style scoped>
.about-page {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.team {
  margin: 2rem 0;
}

.members {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 1rem;
}

.member-card {
  background: #f9f9f9;
  padding: 1rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.tech-stack {
  margin: 2rem 0;
}
</style>