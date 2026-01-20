export enum UserRole {
  Guest = "guest",
  User = "user",
  Admin = "admin",
}

export interface User {
  id: string;
  name: string;
  email: string;
  roles: UserRole[];
  createdAt?: string;
}
