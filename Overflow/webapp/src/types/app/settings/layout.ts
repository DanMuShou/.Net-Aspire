export const Layout = {
  focus: "focus",
  workbench: "workbench",
  none: "none",
} as const;

export type LayoutType = (typeof Layout)[keyof typeof Layout];
