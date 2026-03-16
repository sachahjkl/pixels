<script lang="ts">
	import { Layer } from 'svelte-canvas';

	type CursorLayerProps = {
		mouseGridPos: { x: number; y: number } | 'unset';
		offset: { x: number; y: number };
		scale: number;
		pixelSize: number;
	};

	const { mouseGridPos, offset, scale, pixelSize }: CursorLayerProps = $props();
</script>

{#if mouseGridPos !== 'unset'}
	<Layer
		render={function (opts) {
			const ctx = opts.context;
			ctx.save();
			ctx.translate(offset.x, offset.y);
			ctx.scale(scale, scale);

			const x = mouseGridPos.x * pixelSize;
			const y = mouseGridPos.y * pixelSize;

			ctx.strokeStyle = 'black';
			ctx.lineWidth = 3 / scale;
			ctx.strokeRect(x, y, pixelSize, pixelSize);

			ctx.strokeStyle = 'white';
			ctx.lineWidth = 1.5 / scale;
			ctx.strokeRect(x, y, pixelSize, pixelSize);

			ctx.restore();
		}}
	/>
{/if}
