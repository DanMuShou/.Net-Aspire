<<<<<<< HEAD:Overflow/webapp/src/router/guards.ts
// 路由守卫
=======
import type { RouteInfoInterface } from '@/types/app/settings/route'
>>>>>>> new:Overflow/WebApp/src/router/guards.ts
import { useAuthStore } from '../stores'
import type { NavigationGuard } from 'vue-router'

export const authGuard: NavigationGuard = async (to, from, next) => {
  const authStore = useAuthStore()
  const routeInfo = to.meta.data as RouteInfoInterface

  if (routeInfo.auth && !authStore.isAuthenticated) {
    // 如果需要认证但用户未登录，重定向到登录页
    next({
      path: '/login',
      query: { redirect: to.fullPath },
    })
  } else {
<<<<<<< HEAD:Overflow/webapp/src/router/guards.ts
    // 检查角色权限
    const requiresRoles = to.meta.metaData as 
=======
    const requiresRoles = routeInfo.roles
>>>>>>> new:Overflow/WebApp/src/router/guards.ts
    if (requiresRoles && requiresRoles.length > 0) {
      if (!requiresRoles.some(role => authStore.hasRole(role))) {
        // 用户没有所需角色，重定向到无权限页面
        next({ path: '/unauthorized' })
        return
      }
    }
<<<<<<< HEAD:Overflow/webapp/src/router/guards.ts

=======
>>>>>>> new:Overflow/WebApp/src/router/guards.ts
    next()
  }
}
