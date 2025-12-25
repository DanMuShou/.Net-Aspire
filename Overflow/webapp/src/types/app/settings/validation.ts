import type z from 'zod'

export interface zodValidatorInterface {
  func: z.ZodType
  message?: string
}

export class zodValidator implements zodValidatorInterface {
  func: z.ZodType
  message?: string

  constructor(func: z.ZodType, message?: string) {
    this.func = func
    this.message = message
  }
}
