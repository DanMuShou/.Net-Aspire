import type { RouteRecordRaw } from "vue-router";
import { RouteInfo } from "@/types/app/settings/route";

const loginRoutes: RouteRecordRaw[] = [
  {
    path: "/login",
    name: "Login",
    component: () => import("@/pages/Single/LoginPage.vue"),
    meta: {
      data: new RouteInfo("登录", ""),
    },
  },
];

const homeRoutes: RouteRecordRaw[] = [
  {
    path: "/",
    name: "Home",
    component: () => import("@/pages/Combination/HomePage.vue"),
    meta: { data: new RouteInfo("首页", "", true) },
  },
];

export default [...loginRoutes, ...homeRoutes];
