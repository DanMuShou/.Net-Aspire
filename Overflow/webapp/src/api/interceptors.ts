// API请求拦截器
import { useAuthStore } from "@/stores";
import type { AxiosError } from "axios";

export const requestInterceptor = (config: any) => {
  const authStore = useAuthStore();
  if (authStore.token) {
    config.headers.Authorization = `Bearer ${authStore.token}`;
  }
  return config;
};

export const responseInterceptor = (response: any) => {
  // 对响应数据做点什么
  return response;
};

export const responseErrorInterceptor = (error: AxiosError) => {
  if (error.response?.status === 401) {
    const authStore = useAuthStore();
  }
  return Promise.reject(error);
};
