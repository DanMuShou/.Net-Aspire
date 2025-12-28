<template>
	<v-app>
		<WorkbenchLayout v-if="test">
			<router-view></router-view>
		</WorkbenchLayout>
		<FocusLayout v-else>
			<router-view></router-view>
		</FocusLayout>
	</v-app>
</template>

<script setup>
import WorkbenchLayout from './layouts/WorkbenchLayout.vue'
import FocusLayout from './layouts/FocusLayout.vue'

const test = ref(false)
</script>
