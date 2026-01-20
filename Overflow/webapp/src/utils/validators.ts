import { z } from "zod";
import type { zodValidatorInterface } from "@/types/app/settings/validation";
export const createRules = <T>(
  ...validators: zodValidatorInterface[]
): ((value: T) => boolean | string)[] => {
  return validators.map(valid => {
    return (inputValue: T) => {
      try {
        valid.func.parse(inputValue);
        return true;
      } catch (error: any) {
        return valid.message || error || "验证失败";
      }
    };
  });
};

export const requiredValidate = (): z.ZodString => {
  return z.string().nonempty();
};

export const counterValidate = (min = 0, max = 100): z.ZodNumber => {
  return z.number().min(min).max(max);
};

export const lengthValidate = (min = 0, max = 100): z.ZodString => {
  return z.string().min(min).max(max);
};

export const emailValidate = (): z.ZodEmail => {
  return z.email();
};

export const phoneValidate = (): z.ZodString => {
  return z.string().regex(/^1[3456789]\d{9}$/);
};

export const dateValidate = (): z.ZodDate => {
  return z.date();
};

export const urlValidate = (): z.ZodURL => {
  return z.url();
};

export const uuidValidate = (): z.ZodUUID => {
  return z.uuid();
};

export const passwordValidate = (): z.ZodString => {
  return z
    .string()
    .min(10)
    .max(25)
    .regex(/(?=.*[a-z])/)
    .regex(/(?=.*[A-Z])/)
    .regex(/(?=.*\d)/);
};
