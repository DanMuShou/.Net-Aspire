export enum UserRole {
  Guest = 'guest',
  User = 'user',
  Admin = 'admin',
}

export interface User {
  id: number
  username: string
  email: string
  createdAt: string
}
