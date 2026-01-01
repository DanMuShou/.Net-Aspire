<template>
  <v-container>
    <v-card flat justify="center">
      <v-card-title class="text-center text-h5 pb-6">用户登录</v-card-title>

      <v-card-text>
        <v-form fast-fail @submit.prevent="handleSubmit">
          <div class="text-subtitle-1 text-medium-emphasis">账户</div>
          <v-text-field
            v-model="formData.email"
            density="compact"
            placeholder="邮箱或用户名"
            prepend-inner-icon="mdi-account"
            :rules="[rules.email]"
            type="email"
            variant="outlined"
          />

          <div class="text-subtitle-1 text-medium-emphasis">密码</div>
          <v-text-field
            v-model="formData.password"
            :append-inner-icon="pwdVisible ? 'mdi-eye-off' : 'mdi-eye'"
            class="mb-4"
            density="compact"
            placeholder="请输入密码"
            prepend-inner-icon="mdi-lock"
            :rules="[rules.password]"
            :type="pwdVisible ? 'text' : 'password'"
            variant="outlined"
            @click:append-inner="pwdVisible = !pwdVisible"
          />

          <v-btn block color="primary" :loading="isSubmitting" type="submit">
            登录
          </v-btn>
        </v-form>
      </v-card-text>

      <v-card-actions class="justify-center pt-4">
        <v-btn color="primary" to="/register" variant="text">
          还没有账号？立即注册
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { useMessageStore } from '@/stores/modules/message'
import { getMessage } from '@/utils/message'
import { emailValidate, requiredValidate } from '@/utils/validators'

interface LoginForm {
  email: string
  password: string
}

const formData = reactive<LoginForm>({
  email: '',
  password: '',
})

const isSubmitting = ref(false)
const pwdVisible = ref(false)
const messageStore = useMessageStore()

const rules = {
  email: (value: string): boolean | string => {
    if (!requiredValidate(value)) return '请输入邮箱'
    if (!emailValidate(value)) return '请输入正确的邮箱'
    return true
  },
  password: (value: string): boolean | string => {
    if (!requiredValidate(value)) return '请输入密码'
    return true
  },
}

function validateForm(): boolean {
  return Object.entries(rules).every(([key, validateFn]) => {
    return validateFn(formData[key as keyof LoginForm]) === true
  })
}

async function handleSubmit(): Promise<void> {
  if (!validateForm()) {
    return
  }

  isSubmitting.value = true
  try {
    await new Promise(resolve => setTimeout(resolve, 1000))
    const message = getMessage('登录成功', 'success')
    messageStore.sendMessage(message)
  } catch (error) {
    console.error('登录失败:', error)
  } finally {
    isSubmitting.value = false
  }
}
</script>
