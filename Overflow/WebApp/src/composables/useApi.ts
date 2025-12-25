// API请求的组合式函数
import { ref, onMounted } from 'vue'

export function useApi<T>(apiCall: () => Promise<T>) {
  const data = ref<T | null>(null)
  const loading = ref(true)
  const error = ref<Error | null>(null)

  const execute = async () => {
    try {
      loading.value = true
      error.value = null
      data.value = await apiCall()
    } catch (err) {
      error.value = err instanceof Error ? err : new Error('未知错误')
      console.error('API请求失败:', err)
    } finally {
      loading.value = false
    }
  }

  onMounted(execute)

  return {
    data,
    loading,
    error,
    execute,
  }
}