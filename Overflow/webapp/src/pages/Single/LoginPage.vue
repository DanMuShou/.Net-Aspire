<template>
  <FocusLayout>
    <v-card class="fill-height" flat>
      <v-card-title class="text-center text-h5">用户登录</v-card-title>

      <v-card-text>
        <v-form ref="formRef" @submit.prevent="handleSubmit">
          <div class="text-subtitle-1 text-medium-emphasis">账户</div>
          <v-text-field
            v-model="formData.email"
            density="compact"
            placeholder="邮箱或用户名"
            prepend-inner-icon="mdi-account"
            :rules="rules.email"
            type="email"
            variant="outlined"
          />

          <div
            class="d-flex text-subtitle-1 text-medium-emphasis align-center justify-space-between"
          >
            密码
            <a
              class="text-caption text-decoration-none text-blue"
              href="#"
              rel="noopener noreferrer"
              target="_blank"
            >
              忘记密码?
            </a>
          </div>
          <v-text-field
            v-model="formData.password"
            :append-inner-icon="pwdVisible ? 'mdi-eye-off' : 'mdi-eye'"
            class="mb-4"
            density="compact"
            placeholder="请输入密码"
            prepend-inner-icon="mdi-lock"
            :rules="rules.password"
            :type="pwdVisible ? 'text' : 'password'"
            variant="outlined"
            @click:append-inner="pwdVisible = !pwdVisible"
          />

          <v-btn block color="primary" :loading="isSubmitting" type="submit">
            登录
          </v-btn>
        </v-form>
      </v-card-text>

      <v-card-actions class="justify-center">
        <v-btn color="primary" to="/register" variant="text">
          还没有账号？立即注册
        </v-btn>
      </v-card-actions>
    </v-card>
  </FocusLayout>
</template>

<script setup lang="ts">
import type { VForm } from 'vuetify/components';
import FocusLayout from '@/layouts/FocusLayout.vue';
import { useMessageStore } from '@/stores/modules/message';
import { zodValidator } from '@/types/app/settings/validation';
import { getMessage } from '@/utils/message';
import {
  createRules,
  emailValidate,
  requiredValidate,
} from '@/utils/validators';

interface LoginForm {
  email: string;
  password: string;
}

const formData = reactive<LoginForm>({
  email: '',
  password: '',
});

const formRef = ref<VForm>();
const isSubmitting = ref(false);
const pwdVisible = ref(false);
const messageStore = useMessageStore();

const rules = {
  email: createRules(
    new zodValidator(requiredValidate(), '请输入邮箱'),
    new zodValidator(emailValidate(), '请输入正确的邮箱')
  ),
  password: createRules(
    new zodValidator(requiredValidate(), '请输入密码'),
    new zodValidator(emailValidate(), '请输入正确的邮箱')
  ),
};

async function handleSubmit(): Promise<void> {
  const validationResult = await formRef.value?.validate();
  if (!validationResult || !validationResult.valid) {
    return;
  }

  isSubmitting.value = true;
  try {
    await new Promise(resolve => setTimeout(resolve, 1000));
    messageStore.sendMessage(getMessage('登录成功', 'success'));
  } catch (error) {
    messageStore.sendMessage(getMessage(`登录失败: ${error}`, 'error'));
  } finally {
    isSubmitting.value = false;
  }
}
</script>
