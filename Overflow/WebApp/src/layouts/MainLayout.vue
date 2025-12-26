<template>
	<v-app>
		<v-layout>
			<TopNavigation />
			<LeftNavigation />
			<v-main>
				<v-container fluid>
					<v-row dense>
						<v-col v-for="n in 4" :key="n" cols="12">
							<v-card
								:subtitle="`Subtitle for Content ${n}`"
								:title="`Content ${n}`"
								text="Lorem ipsum dolor sit amet consectetur, adipisicing elit.?"
							></v-card>
						</v-col>
					</v-row>
				</v-container>
			</v-main>
		</v-layout>
	</v-app>
</template>

<script setup>
import TopNavigation from '@/components/common/navigation/TopNavigation.vue';
import LeftNavigation from '@/components/common/drawerNav/LeftNavigation.vue';
</script>
