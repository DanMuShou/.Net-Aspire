import type { User } from "@/types/api/keycloak/user";

import { defineStore } from "pinia";
import { userApi } from "@/api/modules/user";

export const useAuthStore = defineStore(
  "auth",
  () => {
    const user = ref<User | null>(null);
    const token = ref<string | null>(null);
    const refreshToken = ref<string | null>(null);

    const isAuthenticated = computed(() => !!token.value);
    const hasRole = (role: string[]): boolean => {
      return true;
    };

    // 操作
    const login = async (credentials: { username: string; password: string }): Promise<void> => {
      try {
        await new Promise((resolve) => setTimeout(resolve, 3000));
        const response = await userApi.login(credentials);
        token.value = response.token;
      } catch (error) {
        console.error("登录失败:", error);
        throw error;
      }
    };

    const logout = async (): Promise<void> => {
      try {
        await userApi.logout();
      } catch (error) {
        console.error("退出失败:", error);
      } finally {
        token.value = null;
        refreshToken.value = null;
        user.value = null;
      }
    };

    const fetchUserInfo = async (): Promise<void> => {
      try {
        const userInfo = await userApi.getUserInfo();
        user.value = userInfo;
      } catch (error) {
        console.error("获得用户信息失败:", error);
        throw error;
      }
    };

    const init = async (): Promise<void> => {
      if (token.value) {
        try {
          await fetchUserInfo();
        } catch {
          console.warn("Token 可能已失效，执行登出操作");
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
      key: "auth",
      storage: localStorage,
      pick: ["token", "refreshToken"],
    },
  },
);
