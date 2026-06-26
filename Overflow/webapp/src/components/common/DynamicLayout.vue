<template>
  <div v-if="currentLayout === 'none'">
    <router-view />
  </div>
  <component :is="currentLayout" v-else>
    <template v-slot="slotProps">
      <router-view v-bind="slotProps" />
    </template>

    <!-- 为WorkbenchLayout提供侧边栏内容 -->
    <template v-if="isWorkbenchLayout" #sideNav>
      <v-list nav>
        <v-list-item
          v-for="item in navigationItems"
          :key="item.title"
          :active="isActiveRoute(item.to)"
          :prepend-icon="item.icon"
          :title="item.title"
          :to="item.to"
        />
      </v-list>
    </template>
  </component>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";
import FocusLayout from "@/layouts/FocusLayout.vue";
import WorkbenchLayout from "@/layouts/WorkbenchLayout.vue";
import type { LayoutType } from "@/types/app/settings/layout";

// 导航菜单项
const navigationItems = [
  { title: "首页", icon: "mdi-home", to: "/" },
  { title: "问题", icon: "mdi-help-circle", to: "/questions" },
  { title: "标签", icon: "mdi-tag", to: "/tags" },
  { title: "用户", icon: "mdi-account-group", to: "/users" },
];

const route = useRoute();

// 获取当前路由的布局类型
const layoutType = computed<LayoutType>(() => {
  return (route.meta.data as { layout?: LayoutType })?.layout || "workbench";
});

// 根据布局类型选择组件
const currentLayout = computed(() => {
  switch (layoutType.value) {
    case "focus": {
      return FocusLayout;
    }
    case "workbench": {
      return WorkbenchLayout;
    }
    case "none": {
      // 如果是none布局，则直接渲染内容
      return "none";
    }
    default: {
      return WorkbenchLayout;
    }
  }
});

// 判断是否为WorkbenchLayout
const isWorkbenchLayout = computed(() => {
  return layoutType.value === "workbench";
});

// 检查当前路由是否激活
function isActiveRoute(path: string): boolean {
  return route.path === path;
}
</script>
