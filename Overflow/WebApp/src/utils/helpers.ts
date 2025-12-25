// 辅助函数

/**
 * 格式化日期
 * @param date 日期
 * @param format 格式
 * @returns 格式化后的日期字符串
 */
export const formatDate = (date: Date | string | number, format = 'YYYY-MM-DD HH:mm:ss'): string => {
  const d = new Date(date)
  const year = d.getFullYear()
  const month = String(d.getMonth() + 1).padStart(2, '0')
  const day = String(d.getDate()).padStart(2, '0')
  const hours = String(d.getHours()).padStart(2, '0')
  const minutes = String(d.getMinutes()).padStart(2, '0')
  const seconds = String(d.getSeconds()).padStart(2, '0')

  return format
    .replace('YYYY', String(year))
    .replace('MM', month)
    .replace('DD', day)
    .replace('HH', hours)
    .replace('mm', minutes)
    .replace('ss', seconds)
}

/**
 * 防抖函数
 * @param func 函数
 * @param wait 等待时间
 * @returns 防抖后的函数
 */
export const debounce = <T extends (...args: any[]) => any>(func: T, wait: number) => {
  let timeout: NodeJS.Timeout | null = null
  return function (...args: Parameters<T>) {
    if (timeout) clearTimeout(timeout)
    timeout = setTimeout(() => func.apply(this, args), wait)
  }
}

/**
 * 节流函数
 * @param func 函数
 * @param limit 限制时间
 * @returns 节流后的函数
 */
export const throttle = <T extends (...args: any[]) => any>(func: T, limit: number) => {
  let inThrottle: boolean
  return function (...args: Parameters<T>) {
    if (!inThrottle) {
      func.apply(this, args)
      inThrottle = true
      setTimeout(() => (inThrottle = false), limit)
    }
  }
}

/**
 * 深拷贝对象
 * @param obj 要拷贝的对象
 * @returns 拷贝后的对象
 */
export const deepClone = <T>(obj: T): T => {
  if (obj === null || typeof obj !== 'object') return obj
  if (obj instanceof Date) return new Date(obj.getTime()) as any
  if (obj instanceof Array) return obj.map((item) => deepClone(item)) as any
  if (typeof obj === 'object') {
    const clonedObj: any = {}
    Object.keys(obj).forEach((key) => {
      clonedObj[key] = deepClone((obj as any)[key])
    })
    return clonedObj
  }
  return obj
}

/**
 * 生成唯一ID
 * @returns 唯一ID
 */
export const generateId = (): string => {
  return Date.now().toString(36) + Math.random().toString(36).substr(2)
}