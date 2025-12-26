// 示例用户相关的API接口
import axios from 'axios';
import type { User } from '@/types/api/user';

const API_BASE = '/api';

export const userApi = {
	// 获取用户信息
	async getUserInfo(): Promise<User> {
		const response = await axios.get(`${API_BASE}/user`);
		return response.data;
	},

	// 用户登录
	async login(credentials: { username: string; password: string }): Promise<{ token: string }> {
		const response = await axios.post(`${API_BASE}/auth/login`, credentials);
		return response.data;
	},

	// 用户登出
	async logout(): Promise<void> {
		await axios.post(`${API_BASE}/auth/logout`);
	},
};
