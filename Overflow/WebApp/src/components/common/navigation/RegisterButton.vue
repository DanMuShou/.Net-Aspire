<template>
	<v-btn
		variant="elevated"
		rounded="lg"
		size="small"
		@click="handleRegister"
		text=" 注册 "
	></v-btn>
</template>

<script setup>
const handleRegister = () => {
	console.log('register')
}
</script>
