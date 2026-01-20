import router from "@/router";
import vuetify from "./vuetify";
import pinia from "./pinia";

import type { App } from "vue";

export { vuetify, router, pinia };

export function registerPlugins(app: App) {
  app.use(vuetify).use(pinia).use(router);
}
