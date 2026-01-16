import type { User } from "@/types/api/keycloak/user";

export const authApi = {
  async login(
    userName: string,
    password: string
  ): Promise<{ token: string; refreshToken: string }> {
    return {
      token: 'mock-jwt-token-string',
      refreshToken: 'mock-refresh-token-string',
    };
  },

  async refresh(refreshToken: string): Promise<{ token: string, refreshToken: string }> {
    return {
      token: 'mock-jwt-token-string',
      refreshToken: 'mock-refresh-token-string',
    };
  },
  
  async logout() {
    return Promise.resolve();
  },

  async getUserInfo(): Promise<User> {
    return {
      id: 'mock-user-id',
      name: 'mock-user-name',
      email: 'mock@example.com',
      roles: [],
    };
  },
};
