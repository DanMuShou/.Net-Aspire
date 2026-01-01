<template>
	<v-container>
		<v-card justify="center" align="center" flat>
			<v-card-title class="text-center text-h5 pb-6"> 用户登录 </v-card-title>

			<v-card-text>
				<form @submit.prevent="handleLogin">
					<v-text-field
						v-model="formData.email"
						label="邮箱"
						type="email"
						:rules="[rules.required, emailValidate]"
						variant="outlined"
						class="mb-4" />

					<v-text-field
						v-model="formData.password"
						label="密码"
						type="password"
						:rules="[requiredValidate]"
						variant="outlined"
						class="mb-4" />

					<v-btn
						type="submit"
						color="primary"
						block
						size="large"
						:loading="isSubmitting"
						:disabled="!emailValidation.isValid || !passwordValidation.isValid">
						{{ isSubmitting ? '登录中...' : '登录' }}
					</v-btn>
				</form>
			</v-card-text>

			<v-card-actions class="justify-center pt-4">
				<v-btn to="/register" variant="text" color="primary">
					还没有账号？立即注册
				</v-btn>
			</v-card-actions>
		</v-card>
	</v-container>
</template>

<script setup lang="ts">
import { requiredValidate, emailValidate } from '@/utils/validators'

interface LoginForm {
	email: string
	password: string
}

const formData = reactive<LoginForm>({
	email: '',
	password: '',
})

const rules = {
  email: {
    required: requiredValidate,
    email: emailValidate,
    message: {
      required: '请输入邮箱',
      email: '请输入有效的邮箱',
    },
  },
  password: {
    required: requiredValidate,
    message: {
      required: '请输入密码',
    },
  },
}

const isSubmitting = ref(false)


// 验证整个表单
const validateForm = (): boolean => {
}

const handleLogin = async () => {
	if (!validateForm()) {
		return
	}

	isSubmitting.value = true

	try {
		// 这里应该是实际的登录逻辑
		console.log('登录数据:', {
			email: formData.email,
			password: formData.password,
		})
		await new Promise(resolve => setTimeout(resolve, 1000))

	} catch (error) {
		console.error('登录失败:', error)
		// 这里可以显示错误消息给用户
	} finally {
		isSubmitting.value = false
	}
}
</script>
