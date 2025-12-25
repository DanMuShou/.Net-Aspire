import { createRouter, createWebHistory } from 'vue-router'
import { authGuard } from './guards'
import moduleRoutes from './modules'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    ...moduleRoutes,
    // 404页面路由
    {
      path: '/:pathMatch(.*)*',
      name: 'NotFound',
      component: () => import('@/pages/NotFoundPage.vue'),
    },
  ],
})

// 全局路由守卫
router.beforeEach(authGuard)

export default router