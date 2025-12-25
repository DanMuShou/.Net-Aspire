// 插件配置入口
import type { App } from 'vue'

// vuetify UI库
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css'
import 'vuetify/styles'
import router from '@/router'

const vuetify = createVuetify({
  components,
  directives,
})

export function setupPlugins(app: App) {
  // 这里可以注册全局插件
  // 例如: UI库、全局组件等

  // 注册全局组件示例
  // app.component('GlobalComponent', GlobalComponent)

  // 注册UI库
  app.use(vuetify)

  // 注册路由
  app.use(router)
}
