import { AlertMessage, type IAlertMessage } from '@/types/app/alertMessage'

export const getInfoMessage = (
  message: string,
  title: string = ''
): IAlertMessage => {
  return {
    ...AlertMessage,
    type: 'info',
    title: title,
    message,
  }
}

export const getSuccessMessage = (
  message: string,
  title: string = ''
): IAlertMessage => {
  return {
    ...AlertMessage,
    type: 'success',
    title: title,
    message,
  }
}

export const getWarningMessage = (
  message: string,
  title: string = ''
): IAlertMessage => {
  return {
    ...AlertMessage,
    type: 'warning',
    title: title,
    message,
  }
}

export const getErrorMessage = (
  message: string,
  title: string = ''
): IAlertMessage => {
  return {
    ...AlertMessage,
    type: 'error',
    title: title,
    message,
  }
}

export const getMessage = (
  message: string,
  type: 'error' | 'info' | 'success' | 'warning' | undefined,
  title: string = ''
): IAlertMessage => {
  return {
    ...AlertMessage,
    type,
    title: title,
    message,
  }
}
