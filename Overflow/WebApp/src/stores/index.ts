// 状态管理主入口
import type { App } from 'vue'
import { createPinia } from 'pinia'

const store = createPinia()

export function setupStore(app: App) {
  app.use(store)
}

export { store }
export * from './modules/auth'
export * from './modules/common'