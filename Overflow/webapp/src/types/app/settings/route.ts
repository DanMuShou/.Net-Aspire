import type { UserRoleType } from "@/types/api/keycloak/user";
import type { LayoutType } from "@/types/app/settings/layout";
import { Layout } from "@/types/app/settings/layout";

export interface RouteInfoInterface {
  title: string;
  icon?: string;
  auth?: boolean;
  roles?: UserRoleType[];
  layout?: LayoutType;
}

export class RouteInfo implements RouteInfoInterface {
  title: string;
  icon?: string;
  auth?: boolean;
  roles?: UserRoleType[];
  layout?: LayoutType;

  constructor(
    title: string,
    layout: LayoutType,
    icon?: string,
    auth?: boolean,
    roles?: UserRoleType[],
  ) {
    this.title = title;
    this.layout = layout;
    this.icon = icon;
    this.auth = auth;
    this.roles = roles;
  }
}

export const emptyRouteMeta: RouteInfoInterface = new RouteInfo(
  "",
  Layout.workbench,
  "",
  false,
  [],
);
