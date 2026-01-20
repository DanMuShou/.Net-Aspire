import App from "./App.vue";
import { registerPlugins } from "./plugins";
import "unfonts.css";

interface a {}

function stasdAAA(a: any): string {
  return "stasdAAA" + a;
}

console.log("a", {} as a);
console.log("stasdAAA", stasdAAA);

const app = createApp(App);

registerPlugins(app);

app.mount("#app");
