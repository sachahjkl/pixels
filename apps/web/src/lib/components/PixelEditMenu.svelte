<script lang="ts">
	import type { Color } from '$lib/pixel';
	import { DEFAULT_PALETTE, type RGB } from '$lib/palette';

	type PixelEditMenuProps = {
		x: number;
		y: number;
		color: Color;
		oncolorchange: (color: Color) => void;
		onclose: () => void;
	};

	const { x, y, color, oncolorchange, onclose }: PixelEditMenuProps = $props();

	function rgbToColor(rgb: RGB): Color {
		const hex = (n: number) => n.toString(16).padStart(2, '0').toUpperCase();
		return `#${hex(rgb.r)}${hex(rgb.g)}${hex(rgb.b)}`;
	}

	const paletteEntries = Object.values(DEFAULT_PALETTE).map(rgbToColor);
</script>

<div class="pointer-events-none fixed inset-0 flex items-end justify-center pb-6">
	<div
		class="pointer-events-auto flex flex-col gap-3 rounded-xl border border-neutral-700 bg-neutral-900/95 p-4 shadow-2xl backdrop-blur-sm"
	>
		<div class="flex items-center gap-3">
			<div class="h-6 w-6 rounded" style="background-color: {color}"></div>
			<span class="font-mono text-xs text-neutral-400">
				Pixel <span class="text-white">{x}, {y}</span>
			</span>
			<span class="font-mono text-xs text-neutral-300">{color}</span>
			<button
				onclick={onclose}
				class="ml-auto cursor-pointer text-neutral-500 transition-colors hover:text-white"
				aria-label="Close"
			>✕</button>
		</div>
		<div class="grid grid-cols-16 gap-1" style="grid-template-columns: repeat(16, 1.5rem);">
			{#each paletteEntries as paletteColor}
				<button
					class="h-6 w-6 rounded transition-transform hover:scale-110 {paletteColor === color ? 'scale-125' : ''}"
					style="background-color: {paletteColor}"
					onclick={() => oncolorchange(paletteColor)}
					title={paletteColor}
				></button>
			{/each}
		</div>
	</div>
</div>
