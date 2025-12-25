// 路由守卫
import { useAuthStore } from '@/stores'
import type { NavigationGuard } from 'vue-router'

export const authGuard: NavigationGuard = async (to, from, next) => {
  const authStore = useAuthStore()
  const requiresAuth = to.meta.requiresAuth === true

  if (requiresAuth && !authStore.isAuthenticated) {
    // 如果需要认证但用户未登录，重定向到登录页
    next({
      path: '/login',
      query: { redirect: to.fullPath },
    })
  } else {
    // 检查角色权限
    const requiresRoles = to.meta.requiresRoles as string[]
    if (requiresRoles && requiresRoles.length > 0) {
      if (!requiresRoles.some((role) => authStore.hasRole(role))) {
        // 用户没有所需角色，重定向到无权限页面
        next({ path: '/unauthorized' })
        return
      }
    }
    
    next()
  }
}