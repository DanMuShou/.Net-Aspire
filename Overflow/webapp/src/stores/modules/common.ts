// 通用状态管理模块
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useCommonStore = defineStore('common', () => {
  // 状态
  const loading = ref(false)
  const snackbar = ref({
    show: false,
    message: '',
    color: 'info',
    timeout: 3000,
  })

  // 操作
  const setLoading = (status: boolean) => {
    loading.value = status
  }

  const showSnackbar = (message: string, color = 'info', timeout = 3000) => {
    snackbar.value = {
      show: true,
      message,
      color,
      timeout,
    }
  }

  const hideSnackbar = () => {
    snackbar.value.show = false
  }

  return {
    loading,
    snackbar,
    setLoading,
    showSnackbar,
    hideSnackbar,
  }
})