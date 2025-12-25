import { UserRole } from '@/types/api/keycloak/user'

export interface RouteInfoInterface {
  title: string
  icon?: string
  auth?: boolean
  roles?: UserRole[]
}

export class RouteInfo implements RouteInfoInterface {
  title: string
  icon?: string
  auth?: boolean
  roles?: UserRole[]

  constructor(
    title: string,
    icon?: string,
    auth?: boolean,
    roles?: UserRole[]
  ) {
    this.title = title
    this.icon = icon
    this.auth = auth
    this.roles = roles
  }
}

export const emptyRouteMeta: RouteInfoInterface = new RouteInfo(
  '',
  '',
  false,
  []
)
