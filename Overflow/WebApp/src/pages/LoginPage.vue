<template>
	<v-container class="fill-height">
		<v-card class="fill-height fill-width" justify="center" align="center">
			<v-card-title class="text-center text-h5 pb-6"> 用户登录 </v-card-title>

			<v-card-text>
				<form @submit.prevent="handleLogin">
					<v-text-field
						v-model="formData.email"
						label="邮箱"
						type="email"
						:error="!emailValidation.isValid"
						:error-messages="emailErrorMessage"
						required
						variant="outlined"
						class="mb-4"
						@blur="validateEmailField" />

					<v-text-field
						v-model="formData.password"
						label="密码"
						type="password"
						:error="!passwordValidation.isValid"
						:error-messages="passwordErrorMessage"
						required
						variant="outlined"
						class="mb-4"
						@blur="validatePasswordField" />

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
import { ref, reactive, computed } from 'vue'
import { useRouter } from 'vue-router'
import {
	validateEmail,
	validatePassword,
	type EmailValidationResult,
	type PasswordValidationResult,
} from '@/utils/validators'

interface LoginForm {
	email: string
	password: string
}

const router = useRouter()
const formData = reactive<LoginForm>({
	email: '',
	password: '',
})

const emailValidation = ref<EmailValidationResult>({ isValid: true })
const passwordValidation = ref<PasswordValidationResult>({ isValid: true })
const isSubmitting = ref(false)

// 计算属性用于错误消息
const emailErrorMessage = computed(() => {
	return emailValidation.value.isValid
		? []
		: [emailValidation.value.error || '邮箱格式不正确']
})

const passwordErrorMessage = computed(() => {
	return passwordValidation.value.isValid
		? []
		: [passwordValidation.value.error || '密码格式不正确']
})

// 实时验证邮箱
const validateEmailField = () => {
	emailValidation.value = validateEmail(formData.email)
}

// 实时验证密码
const validatePasswordField = () => {
	passwordValidation.value = validatePassword(formData.password, 6) // 设置最小长度为6
}

// 验证整个表单
const validateForm = (): boolean => {
	validateEmailField()
	validatePasswordField()

	return emailValidation.value.isValid && passwordValidation.value.isValid
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

		// 模拟 API 调用
		await new Promise(resolve => setTimeout(resolve, 1000))

		// 登录成功后跳转
		router.push('/dashboard') // 或其他默认页面
	} catch (error) {
		console.error('登录失败:', error)
		// 这里可以显示错误消息给用户
	} finally {
		isSubmitting.value = false
	}
}
</script>
