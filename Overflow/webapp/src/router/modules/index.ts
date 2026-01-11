import type { RouteRecordRaw } from 'vue-router'
import { RouteMeta, type IRouteMeta } from '@/types/app/settings/route'
import { Layouts } from '@/types/app/settings/layout'

const loginRoutes: RouteRecordRaw[] = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/pages/LoginPage.vue'),
    meta: {
      data: new RouteMeta('登录', Layouts.focusLayout),
    },
  },
]

const homeRoutes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/pages/HomePage.vue'),
    meta: { data: new RouteMeta('首页') },
  },
]

export default [...loginRoutes, ...homeRoutes]
