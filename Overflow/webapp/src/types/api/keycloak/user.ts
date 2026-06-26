export const UserRole = {
  Guest: "guest",
  User: "user",
  Admin: "admin",
} as const;

export type UserRoleType = (typeof UserRole)[keyof typeof UserRole];

export interface User {
  id: number;
  username: string;
  email: string;
  createdAt: string;
}
