export interface IAlertMessage {
  title: string
  message: string
  type: 'error' | 'info' | 'success' | 'warning' | undefined
  timeout: number
  close: boolean
}

export const AlertMessage: IAlertMessage = {
  title: '',
  message: '',
  type: 'info',
  timeout: 3000,
  close: true,
}
