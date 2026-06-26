import type { RouteRecordRaw } from "vue-router";
import { RouteInfo } from "@/types/app/settings/route";
import { Layout } from "@/types/app/settings/layout";

const loginRoutes: RouteRecordRaw[] = [
  {
    path: "/login",
    name: "Login",
    component: () => import("@/pages/Single/LoginPage.vue"),
    meta: {
      data: new RouteInfo("登录", Layout.focus, ""),
    },
  },
];

const homeRoutes: RouteRecordRaw[] = [
  {
    path: "/",
    name: "Home",
    component: () => import("@/pages/Combination/HomePage.vue"),
    meta: { data: new RouteInfo("首页", Layout.workbench, "", true) },
  },
];

// 添加示例路由
const exampleRoutes: RouteRecordRaw[] = [
  {
    path: "/example",
    name: "Example",
    component: () => import("@/pages/Single/ExamplePage.vue"),
    meta: { data: new RouteInfo("示例页面", Layout.workbench, "", true) },
  },
  {
    path: "/focus-example",
    name: "FocusExample",
    component: () => import("@/pages/Single/ExamplePage.vue"),
    meta: { data: new RouteInfo("焦点布局示例", Layout.focus, "", true) },
  },
];

export default [...loginRoutes, ...homeRoutes, ...exampleRoutes];