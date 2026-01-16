// 认证状态管理模块
import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import { authApi } from '@/api/modules/keycloak/auth';
import type { User } from '@/types/api/keycloak/user';
import { useMessageStore } from './message';
import { getErrorMessage } from '@/utils/message';
import { fa } from 'zod/locales';

const messageStore = useMessageStore();

export const useAuthStore = defineStore(
  'auth',
  () => {
    const user = ref<User | undefined>(undefined);
    const token = ref<string | undefined>(undefined);
    const refreshToken = ref<string | undefined>(undefined);

    const isAuthenticated = computed(() => !!token.value);
    const hasRole = (role: string) => {
      return true;
    };

    const login = async (username: string, password: string): Promise<void> => {
      try {
        const response = await authApi.login(username, password);
        token.value = response.token;
        refreshToken.value = response.refreshToken;
      } catch (error) {
        messageStore.sendMessage(getErrorMessage('登录失败'));
        throw error;
      }
    };

    const refresh = async (): Promise<boolean> => {
      try {
        if (!refreshToken.value) return false;

        const response = await authApi.refresh(refreshToken.value!);
        token.value = response.token;
        refreshToken.value = response.refreshToken;
        return true;
      } catch (error) {
        messageStore.sendMessage(getErrorMessage('认证失败, 请重新登录.'));
        return false;
      }
    };

    const fetchUserInfo = async (): Promise<void> => {
      try {
        const userInfo = await authApi.getUserInfo();
        user.value = userInfo;
      } catch (error) {
        messageStore.sendMessage(getErrorMessage('获得用户信息失败'));
        throw error;
      }
    };

    const logout = async (): Promise<void> => {
      try {
        await authApi.logout();
      } catch (error) {
        messageStore.sendMessage(getErrorMessage('登出失败'));
        throw error;
      } finally {
        token.value = undefined;
        refreshToken.value = undefined;
        user.value = undefined;
      }
    };

    const init = async () => {
      if (token.value) {
        try {
          await fetchUserInfo();
        } catch (error) {
          console.warn('Token 可能已失效，执行登出操作');
          await logout();
        }
      }
    };

    return {
      user,
      token,
      refreshToken,
      isAuthenticated,
      hasRole,
      login,
      logout,
      fetchUserInfo,
      init,
    };
  },
  {
    persist: {
      key: 'auth',
      storage: localStorage,
      pick: ['token', 'refreshToken'],
    },
  }
);
