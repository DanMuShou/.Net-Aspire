// API请求拦截器
import { useAuthStore } from '@/stores'

export const requestInterceptor = (config: any) => {
  const authStore = useAuthStore()
  if (authStore.token) {
    config.headers.Authorization = `Bearer ${authStore.token}`
  }
  return config
}

export const responseInterceptor = (response: any) => {
  // 对响应数据做点什么
  return response
}