import { UserRole } from '@/types/api/keycloak/user'
<<<<<<< HEAD
import { Layouts } from './layout'

export interface IRouteMeta {
  title: string
  layout: Layouts
=======

export interface RouteInfoInterface {
  title: string
>>>>>>> new
  icon?: string
  auth?: boolean
  roles?: UserRole[]
}

<<<<<<< HEAD
export class RouteMeta implements IRouteMeta {
  title: string
  layout: Layouts
=======
export class RouteInfo implements RouteInfoInterface {
  title: string
>>>>>>> new
  icon?: string
  auth?: boolean
  roles?: UserRole[]

  constructor(
    title: string,
<<<<<<< HEAD
    layout: Layouts = Layouts.workbenchLayout,
=======
>>>>>>> new
    icon?: string,
    auth?: boolean,
    roles?: UserRole[]
  ) {
    this.title = title
<<<<<<< HEAD
    this.layout = layout
=======
>>>>>>> new
    this.icon = icon
    this.auth = auth
    this.roles = roles
  }
}

<<<<<<< HEAD
export const emptyRouteMeta: IRouteMeta = new RouteMeta(
  '',
  Layouts._,
=======
export const emptyRouteMeta: RouteInfoInterface = new RouteInfo(
  '',
>>>>>>> new
  '',
  false,
  []
)
