import type { RouteInfoInterface } from "@/types/app/settings/route";
import { useAuthStore } from "../stores";
import type { NavigationGuard } from "vue-router";

export const authGuard: NavigationGuard = async (to, from, next) => {
  const authStore = useAuthStore();
  const routeInfo = to.meta.data as RouteInfoInterface;

  if (routeInfo.auth && !authStore.isAuthenticated) {
    // 如果需要认证但用户未登录，重定向到登录页
    next({
      path: "/login",
      query: { redirect: to.fullPath },
    });
  } else {
    const requiresRoles = routeInfo.roles;
    if (requiresRoles && requiresRoles.length > 0) {
      if (!requiresRoles.some(role => authStore.hasRole(role))) {
        // 用户没有所需角色，重定向到无权限页面
        next({ path: "/unauthorized" });
        return;
      }
    }
    next();
  }
};
