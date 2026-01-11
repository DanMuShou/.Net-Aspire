// 通用类型定义

// API响应类型
export interface ApiResponse<T = any> {
  code: number
  message: string
  data: T
  success: boolean
}

// 分页参数类型
export interface PaginationParams {
  page: number
  pageSize: number
  total: number
}

// 分页响应类型
export interface PaginatedResponse<T> {
  items: T[]
  pagination: PaginationParams
}

// 响应式数据类型
export interface ReactiveData<T> {
  data: T | null
  loading: boolean
  error: string | null
}

// 可选属性类型（部分属性可选）
export type Optional<T, K extends keyof T> = Pick<Partial<T>, K> & Omit<T, K>

// 必需属性类型（部分属性必需）
export type RequiredSome<T, K extends keyof T> = Pick<T, K> &
  Partial<Omit<T, K>>

export enum HttpStatus {
  Success = 200,
  Created = 201,
  BadRequest = 400,
  Unauthorized = 401,
  Forbidden = 403,
  NotFound = 404,
  ServerError = 500,
}
