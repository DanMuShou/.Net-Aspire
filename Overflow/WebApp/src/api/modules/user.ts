import type { User } from "@/types/api/keycloak/user";

export const userApi = {
  // 获取虚拟用户信息
  async getUserInfo(): Promise<User> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve({
          id: Math.floor(Math.random() * 10_000) + 1,
          username: `user${Math.floor(Math.random() * 10_000) + 1}`,
          email: `user${Math.floor(Math.random() * 10_000) + 1}@example.com`,
          createdAt: new Date().toISOString(),
        });
      }, 300); // 模拟网络延迟
    });
  },

  // 虚拟登录
  async login(credentials: { username: string; password: string }): Promise<{ token: string }> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve({ token: `mock-token-${Date.now()}` });
      }, 500); // 模拟网络延迟
    });
  },

  // 虚拟登出
  async logout(): Promise<void> {
    return new Promise((resolve) => {
      setTimeout(() => {
        console.log("User logged out");
        resolve();
      }, 300); // 模拟网络延迟
    });
  },

  // 生成虚拟用户信息
  generateMockUser(): User {
    const randomId = Math.floor(Math.random() * 10_000) + 1;
    const randomUsername = `user${randomId}`;
    const randomEmail = `user${randomId}@example.com`;
    const createdAt = new Date().toISOString();

    return {
      id: randomId,
      username: randomUsername,
      email: randomEmail,
      createdAt,
    };
  },
};
