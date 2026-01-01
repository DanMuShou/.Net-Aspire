// 示例帖子相关的API接口
import axios from 'axios'

const API_BASE = '/api'

interface Post {
  id: number
  title: string
  content: string
  author: string
  createdAt: string
}

export interface PostQuestion extends Post {
  answerCount: number
  viewCount: number
}

export const postApi = {
  // 获取帖子列表
  async getPosts(): Promise<PostQuestion[]> {
    const response = await axios.get(`${API_BASE}/posts`)
    return response.data
  },

  // 获取单个帖子
  async getPostById(id: number): Promise<PostQuestion> {
    const response = await axios.get(`${API_BASE}/posts/${id}`)
    return response.data
  },

  // 创建帖子
  async createPost(post: Omit<Post, 'id' | 'createdAt'>): Promise<Post> {
    const response = await axios.post(`${API_BASE}/posts`, post)
    return response.data
  },

  // 更新帖子
  async updatePost(id: number, post: Partial<Post>): Promise<Post> {
    const response = await axios.put(`${API_BASE}/posts/${id}`, post)
    return response.data
  },

  // 删除帖子
  async deletePost(id: number): Promise<void> {
    await axios.delete(`${API_BASE}/posts/${id}`)
  },
}