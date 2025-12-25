import './assets/main.css'
import './styles/variables.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { setupStore } from './stores'
import { setupPlugins } from './plugins'

import App from './App.vue'
import router from './router'

const app = createApp(App)

// 使用 Pinia 状态管理
setupStore(app)

// 使用插件
setupPlugins(app)

// 使用路由
app.use(router)

app.mount('#app')