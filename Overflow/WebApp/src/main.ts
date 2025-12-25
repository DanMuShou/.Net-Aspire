import { createApp } from 'vue'
import App from './App.vue'
import './assets/main.css'

import { setupPlugins } from './plugins'
import { setupStore } from './stores'

const app = createApp(App)

setupPlugins(app)
setupStore(app)

app.mount('#app')
