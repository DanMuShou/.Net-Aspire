import vuetify from 'eslint-config-vuetify';
import tseslint from 'typescript-eslint'

export default vuetify({
  rules: {
    // Vue特定规则
    '@stylistic/semi': 'off',
    '@stylistic/space-before-function-paren': 'off',
    '@stylistic/comma-dangle': 'off',
    '@stylistic/member-delimiter-style': 'off',
    'vue/script-indent': 'off',
    'unicorn/no-array-reverse': 'off',

    'vue/multi-word-component-names': 'error',
    'vue/component-definition-name-casing': ['error', 'PascalCase'],
    'vue/component-name-in-template-casing': ['error', 'PascalCase'],
    'vue/html-self-closing': [
      'error',
      {
        html: {
          void: 'never',
          normal: 'always',
          component: 'always',
        },
      },
    ],

    // TypeScript规则
    '@typescript-eslint/no-unused-vars': 'error',
    '@typescript-eslint/explicit-function-return-type': 'warn',

    // 通用规则
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
  },
});