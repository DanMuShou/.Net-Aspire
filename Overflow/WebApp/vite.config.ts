import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import vueDevTools from 'vite-plugin-vue-devtools';
import AutoImport from 'unplugin-auto-import/vite';

// https://vite.dev/config/
export default defineConfig({
	plugins: [
		AutoImport({
			imports: ['vue', 'vue-router', 'pinia'],
			dts: true, // 生成类型声明文件
			dirs: [
				'./src/composables', // 自动导入组合式函数
				'./src/stores',
			],
			eslintrc: {
				enabled: true, // 生成 .eslintrc-auto-import.json 文件
			},
		}),
		vue(),
		vueDevTools(),
	],
	resolve: {
		alias: {
			'@': fileURLToPath(new URL('./src', import.meta.url)),
		},
	},
});
