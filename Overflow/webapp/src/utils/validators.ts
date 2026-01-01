import { z } from 'zod'

/**
 * 验证值是否为非空字符串
 * @param value - 待验证的值
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const requiredValidate = (value: any): boolean => {
	try {
		z.string().nonempty().parse(value)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证数字是否在指定范围内
 * @param value - 待验证的数字
 * @param min - 最小值，默认为0
 * @param max - 最大值，默认为100
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const counterValidate = (value: number, min = 0, max = 100): boolean => {
	try {
		z.number().min(min).max(max).parse(value)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证字符串长度是否在指定范围内
 * @param value - 待验证的字符串
 * @param min - 最小长度，默认为0
 * @param max - 最大长度，默认为100
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const lengthValidate = (value: string, min = 0, max = 100): boolean => {
	try {
		z.string().min(min).max(max).parse(value)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证邮箱格式是否正确
 * @param email - 待验证的邮箱字符串
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const emailValidate = (email: string): boolean => {
	try {
		z.email().parse(email)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证手机号格式是否正确（中国手机号格式）
 * @param phone - 待验证的手机号字符串
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const phoneValidate = (phone: string): boolean => {
	try {
		z.string().regex(/^1[3-9]\d{9}$/).parse(phone)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证日期格式是否正确
 * @param date - 待验证的日期字符串
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const dateValidate = (date: string): boolean => {
	try {
		z.date().parse(date)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证URL格式是否正确
 * @param url - 待验证的URL字符串
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const urlValidate = (url: string): boolean => {
	try {
		z.url().parse(url)
		return true
	} catch (error: any) {
		return false
	}
}

/**
 * 验证密码强度是否符合要求（长度10-25位，且包含至少一个小写字母、一个大写字母和一个数字）
 * @param password - 待验证的密码字符串
 * @returns 验证结果，true表示验证通过，false表示验证失败
 */
export const passwordValidate = (password: string): boolean => {
	try {
		z.string()
			.min(10)
			.max(25)
			.regex(/(?=.*[a-z])/)
			.regex(/(?=.*[A-Z])/)
			.regex(/(?=.*\d)/)
			.parse(password)
		return true
	} catch (error: any) {
		return false
	}
}