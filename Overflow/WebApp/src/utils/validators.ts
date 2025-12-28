// 导入 zod
import { z } from 'zod'

// 定义验证结果接口
export interface ValidationResult<T = unknown> {
	isValid: boolean
	value?: T
	error?: string
}

export interface EmailValidationResult extends ValidationResult<string> {}
export interface PhoneValidationResult extends ValidationResult<string> {}
export interface PasswordValidationResult extends ValidationResult<string> {}
export interface UrlValidationResult extends ValidationResult<string> {}
export interface NumberValidationResult extends ValidationResult<number> {}
export interface StringValidationResult extends ValidationResult<string> {}

export const validateEmail = (email: string): EmailValidationResult => {
	try {
		const parsedValue = z.email('请输入有效的邮箱地址').parse(email)
		return {
			isValid: true,
			value: parsedValue,
		}
	} catch (error: any) {
		return {
			isValid: false,
			error: error.issues?.[0]?.message || '邮箱格式不正确',
		}
	}
}

export const validatePhone = (phone: string): PhoneValidationResult => {
	try {
		const parsedValue = z
			.string()
			.regex(/^1[3-9]\d{9}$/, '请输入有效的手机号码')
			.parse(phone)
		return {
			isValid: true,
			value: parsedValue,
		}
	} catch (error: any) {
		return {
			isValid: false,
			error: error.issues?.[0]?.message || '手机号格式不正确',
		}
	}
}

export const validatePassword = (
	password: string,
	minLength = 8
): PasswordValidationResult => {
	try {
		const parsedValue = z
			.string()
			.min(minLength, `密码长度至少为 ${minLength} 位`)
			.regex(/[A-Z]/, '密码必须包含至少一个大写字母')
			.regex(/[a-z]/, '密码必须包含至少一个小写字母')
			.regex(/\d/, '密码必须包含至少一个数字')
			.parse(password)
		return {
			isValid: true,
			value: parsedValue,
		}
	} catch (error: any) {
		return {
			isValid: false,
			error:
				error.issues?.[0]?.message ||
				`密码格式不正确，必须至少${minLength}位并包含大小写字母和数字`,
		}
	}
}

export const validateUrl = (url: string): UrlValidationResult => {
	try {
		const parsedValue = z.url('请输入有效的 URL 地址').parse(url)
		return {
			isValid: true,
			value: parsedValue,
		}
	} catch (error: any) {
		return {
			isValid: false,
			error: error.issues?.[0]?.message || 'URL 格式不正确',
		}
	}
}

export const isValidNumber = (value: any): NumberValidationResult => {
	try {
		const parsedValue = z
			.preprocess(val => {
				const num = Number(val)
				return isNaN(num) ? undefined : num
			}, z.number())
			.parse(value)
		return {
			isValid: true,
			value: parsedValue,
		}
	} catch (error: any) {
		return {
			isValid: false,
			error: error.issues?.[0]?.message || '数值格式不正确',
		}
	}
}

export const validateStr = (
	name: string,
	minLength = 2,
	maxLength = 20
): StringValidationResult => {
	try {
		const parsedValue = z
			.string()
			.min(minLength, `字符串长度至少为 ${minLength}`)
			.max(maxLength, `字符串长度不能超过 ${maxLength}`)
			.parse(name)
		return {
			isValid: true,
			value: parsedValue,
		}
	} catch (error: any) {
		return {
			isValid: false,
			error:
				error.issues?.[0]?.message ||
				`字符串长度必须在 ${minLength} 到 ${maxLength} 之间`,
		}
	}
}
