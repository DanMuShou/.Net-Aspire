import eslint from '@eslint/js';
import vuetify from 'eslint-config-vuetify';
import tseslint from 'typescript-eslint';

export default vuetify({
  plugins: [tseslint],
  rules: {
    ...eslint.configs.recommended.rules,
    ...tseslint.configs.strict.rules,
    ...tseslint.configs.stylistic.rules,

    '@stylistic/semi': 'off',
    '@stylistic/space-before-function-paren': 'off',
    
    'vue/script-indent': 'off'
    
    '@typescript-eslint/explicit-function-return-type': 'error',
    '@typescript-eslint/no-unused-vars': 'error',
    '@typescript-eslint/no-explicit-any': 'warn',
  },
});
