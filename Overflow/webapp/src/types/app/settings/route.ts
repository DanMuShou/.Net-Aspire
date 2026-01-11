import { UserRole } from '@/types/api/keycloak/user'
import { Layouts } from './layout'

export interface IRouteMeta {
  title: string
  layout: Layouts
  icon?: string
  auth?: boolean
  roles?: UserRole[]
}

export class RouteMeta implements IRouteMeta {
  title: string
  layout: Layouts
  icon?: string
  auth?: boolean
  roles?: UserRole[]

  constructor(
    title: string,
    layout: Layouts = Layouts.workbenchLayout,
    icon?: string,
    auth?: boolean,
    roles?: UserRole[]
  ) {
    this.title = title
    this.layout = layout
    this.icon = icon
    this.auth = auth
    this.roles = roles
  }
}

export const emptyRouteMeta: IRouteMeta = new RouteMeta(
  '',
  Layouts._,
  '',
  false,
  []
)
