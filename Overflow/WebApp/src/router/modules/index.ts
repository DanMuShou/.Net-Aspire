// 路由模块入口
import type { RouteRecordRaw } from 'vue-router'

// 首页路由
const homeRoutes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'Home',
    component: () => import('@/pages/HomePage.vue'),
    meta: { title: '首页' },
  },
]

// 关于页面路由
const aboutRoutes: RouteRecordRaw[] = [
  {
    path: '/about',
    name: 'About',
    component: () => import('@/pages/AboutPage.vue'),
    meta: { title: '关于', requiresAuth: false },
  },
]

// 帖子相关路由
const postRoutes: RouteRecordRaw[] = [
  {
    path: '/posts',
    name: 'Posts',
    component: () => import('@/pages/PostListPage.vue'),
    meta: { title: '帖子列表', requiresAuth: false },
  },
  {
    path: '/post/:id',
    name: 'PostDetail',
    component: () => import('@/pages/PostDetailPage.vue'),
    meta: { title: '帖子详情', requiresAuth: false },
    props: true,
  },
]

// 用户相关路由
const userRoutes: RouteRecordRaw[] = [
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/pages/LoginPage.vue'),
    meta: { title: '登录', requiresAuth: false },
  },
  {
    path: '/profile',
    name: 'Profile',
    component: () => import('@/pages/ProfilePage.vue'),
    meta: { title: '个人资料', requiresAuth: true },
  },
]

export default [
  ...homeRoutes,
  ...aboutRoutes,
  ...postRoutes,
  ...userRoutes,
]