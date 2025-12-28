// 认证状态管理模块
import { defineStore } from "pinia";
import { ref, computed } from "vue";
import { userApi } from "@/api/modules/user";
import type { User } from "../../types/api/user";

export const useAuthStore = defineStore(
  "auth",
  () => {
    const user = ref<User | null>(null);
    const token = ref<string | null>(null);
    const refreshToken = ref<string | null>(null);

    const isAuthenticated = computed(() => !!token.value);
    const hasRole = (role: string) => {
      return true;
    };

    // 操作
    const login = async (credentials: {
      username: string;
      password: string;
    }) => {
      try {
        const response = await userApi.login(credentials);
        token.value = response.token;
        return response;
      } catch (error) {
        console.error("登录失败: ", error);
        throw error;
      }
    };

    const logout = async () => {
      try {
        await userApi.logout();
      } catch (error) {
        console.error("退出失败: ", error);
      } finally {
        token.value = null;
        refreshToken.value = null;
        user.value = null;
      }
    };

    const fetchUserInfo = async () => {
      try {
        const userInfo = await userApi.getUserInfo();
        user.value = userInfo;
        return userInfo;
      } catch (error) {
        console.error("获得用户信息失败: ", error);
        throw error;
      }
    };

    const init = async () => {
      if (token.value) {
        try {
          await fetchUserInfo();
        } catch (error) {
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
  }
);
