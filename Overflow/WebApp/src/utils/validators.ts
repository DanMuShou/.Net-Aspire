// 验证函数

/**
 * 验证邮箱格式
 * @param email 邮箱地址
 * @returns 是否为有效邮箱
 */
export const validateEmail = (email: string): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

/**
 * 验证手机号格式
 * @param phone 手机号
 * @returns 是否为有效手机号
 */
export const validatePhone = (phone: string): boolean => {
  const phoneRegex = /^1[3-9]\d{9}$/
  return phoneRegex.test(phone)
}

/**
 * 验证密码强度
 * @param password 密码
 * @param minLength 最小长度
 * @returns 是否为强密码
 */
export const validatePassword = (password: string, minLength = 8): boolean => {
  if (password.length < minLength) return false

  // 至少包含一个大写字母、一个小写字母和一个数字
  const hasUpperCase = /[A-Z]/.test(password)
  const hasLowerCase = /[a-z]/.test(password)
  const hasNumber = /\d/.test(password)
  
  return hasUpperCase && hasLowerCase && hasNumber
}

/**
 * 验证是否为URL
 * @param url URL字符串
 * @returns 是否为有效URL
 */
export const validateUrl = (url: string): boolean => {
  try {
    new URL(url)
    return true
  } catch {
    return false
  }
}

/**
 * 验证是否为有效数字
 * @param value 值
 * @returns 是否为有效数字
 */
export const isValidNumber = (value: any): boolean => {
  return !isNaN(parseFloat(value)) && isFinite(value)
}