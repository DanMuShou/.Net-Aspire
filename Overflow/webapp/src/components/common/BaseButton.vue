<template>
  <button 
    class="base-button" 
    :class="[`variant-${variant}`, { disabled }]"
    :disabled="disabled"
  >
    <slot />
  </button>
</template>

<script setup lang="ts">
interface Props {
  variant?: 'primary' | 'secondary' | 'danger' | 'outline'
  disabled?: boolean
}

withDefaults(defineProps<Props>(), {
  variant: 'primary',
  disabled: false,
})
</script>

<style scoped>
.base-button {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1rem;
  transition: all 0.2s ease;
}

.base-button:focus {
  outline: 2px solid #3498db;
}

.base-button:not(.disabled):hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0,0,0,0.1);
}

.base-button.disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.variant-primary {
  background-color: #3498db;
  color: white;
}

.variant-secondary {
  background-color: #95a5a6;
  color: white;
}

.variant-danger {
  background-color: #e74c3c;
  color: white;
}

.variant-outline {
  background-color: transparent;
  color: #3498db;
  border: 1px solid #3498db;
}
</style>