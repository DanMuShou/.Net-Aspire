import { AlertMessage, type IAlertMessage } from "@/types/app/ui/alertMessage";

export const useMessageStore = defineStore("messageInfo", () => {
  const index = ref(0);
  const messageInfo = ref<IAlertMessage>({
    ...AlertMessage,
    title: "初始化",
    message: "消息初始化成功",
  });

  const sendMessage = (message: IAlertMessage) => {
    index.value += 1;
    messageInfo.value = message;
  };

  return {
    index,
    messageInfo,
    sendMessage,
  };
});
