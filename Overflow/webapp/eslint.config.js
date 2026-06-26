import eslint from "@eslint/js";
import vuetify from "eslint-config-vuetify";

export default vuetify({
  rules: {
    // ...eslint.configs.recommended.rules,

    "@stylistic/semi": "off",
    "@stylistic/quotes": "off",
    "@stylistic/member-delimiter-style": "off",
    "@stylistic/operator-linebreak": "off",
    "@stylistic/arrow-parens": "off",
    "@stylistic/space-before-function-paren": "off",

    "vue/script-indent": "off",

    "@typescript-eslint/no-explicit-any": "warn",
    "@typescript-eslint/no-use-before-define": "off",
    "@typescript-eslint/no-non-null-assertion": "off",
    "@typescript-eslint/explicit-function-return-type": "error",

    "no-undef": "off",
    "no-unused-vars": "warn",
    "perfectionist/sort-imports": "off",
  },
});
