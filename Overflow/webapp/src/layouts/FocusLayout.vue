<template>
  <v-layout>
    <v-main class="d-flex justify-center align-center">
      <v-container class="elevation-4 rounded-xl" height="80vh" width="60vw">
        <slot name="default" />
      </v-container>
    </v-main>
  </v-layout>
</template>

<script setup></script>

<style lang="scss" scoped>
.infinite-scroll-bg {
  background-image: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="4" height="4" viewBox="0 0 4 4"><rect fill="%23f0f0f0" width="4" height="4"/><path fill="%23d0d0d0" d="M1 3h1v1H1V3zm2-2h1v1H3V1z"/></svg>');
  background-size: 4px 4px;
  animation: scroll-bg 20s linear infinite;
}

@keyframes scroll-bg {
  0% {
    background-position: 0 0;
  }
  100% {
    background-position: 4px 4px;
  }
}

.centered-card {
  // 添加一些样式优化，如阴影和圆角
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
}
</style>
