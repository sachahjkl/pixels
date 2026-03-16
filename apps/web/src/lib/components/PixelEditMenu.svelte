<script lang="ts">
	import type { Color, Pixel } from '$lib/pixel';

	type PixelEditMenuProps = {
		x: number;
		y: number;
		color: Color;
	};

	const { x, y, color }: PixelEditMenuProps = $props();
</script>

<div class="fixed bottom-0 bg-white text-3xl">
	{color}
	{x}
	{y}
</div>
