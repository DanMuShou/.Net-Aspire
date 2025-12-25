// 认证状态管理模块
import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { userApi } from '@/api/modules/user'
import type { User } from '@/api/modules/user'

export const useAuthStore = defineStore(
  'auth',
  () => {
    // 状态
    const user = ref<User | null>(null)
    const token = ref<string | null>(localStorage.getItem('access_token'))
    const refreshToken = ref<string | null>(localStorage.getItem('refresh_token'))

    // 计算属性
    const isAuthenticated = computed(() => !!token.value)
    const hasRole = (role: string) => {
      // 这里可以根据实际需求实现角色检查逻辑
      // 示例中我们暂时返回true，实际应用中应该根据用户角色进行判断
      return true
    }

    // 操作
    const login = async (credentials: { username: string; password: string }) => {
      try {
        const response = await userApi.login(credentials)
        token.value = response.token
        localStorage.setItem('access_token', response.token)
        return response
      } catch (error) {
        console.error('Login failed:', error)
        throw error
      }
    }

    const logout = async () => {
      try {
        await userApi.logout()
      } catch (error) {
        console.error('Logout failed:', error)
      } finally {
        token.value = null
        refreshToken.value = null
        user.value = null
        localStorage.removeItem('access_token')
        localStorage.removeItem('refresh_token')
        localStorage.removeItem('user_info')
      }
    }

    const fetchUserInfo = async () => {
      try {
        const userInfo = await userApi.getUserInfo()
        user.value = userInfo
        localStorage.setItem('user_info', JSON.stringify(userInfo))
        return userInfo
      } catch (error) {
        console.error('Failed to fetch user info:', error)
        throw error
      }
    }

    return {
      user,
      token,
      refreshToken,
      isAuthenticated,
      hasRole,
      login,
      logout,
      fetchUserInfo,
    }
  },
  {
    persist: {
      key: 'auth-store',
      storage: localStorage,
      paths: ['token', 'refreshToken'],
    },
  }
)