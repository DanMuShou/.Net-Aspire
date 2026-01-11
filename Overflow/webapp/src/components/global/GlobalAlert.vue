<template>
  <div class="position-fixed bottom-0 right-0" style="z-index: 9999">
    <v-alert
      v-for="(alert, index) in Array.from(alertMap.values())"
      :key="index"
      border="end"
      class="mb-1"
      closable
      close-label="Close Alert"
      :text="alert.message"
      :title="alert.title"
      :type="alert.type"
    />
  </div>
</template>

<script setup lang="ts">
import type { IAlertMessage } from '@/types/app/alertMessage'
import { useMessageStore } from '@/stores/modules/message'

const alertMap: Map<number, IAlertMessage> = reactive(new Map())

const messageStore = useMessageStore()
watch(
  () => messageStore.index,
  () => {
    alertMap.set(messageStore.index, messageStore.messageInfo)
    console.log('messageStore.index', messageStore.index)
    console.log('messageStore.messageInfo', messageStore.messageInfo)
    console.log('alertMap', alertMap)
    deleteAlert(messageStore.index, messageStore.messageInfo.timeout)
  }
)

function deleteAlert(id: number, timeout: number): void {
  setTimeout(() => {
    alertMap.delete(id)
  }, timeout)
}
</script>
