<script lang="ts">
	import { Layer, type Render } from 'svelte-canvas';
	type PixelProps = {
		width: number;
		height: number;
		x: number;
		y: number;
		color: string;
	};
	const props: PixelProps = $props();
	const render: Render = ({ context, width, height }) => {
		context.fillStyle = props.color;
		context.fillRect(props.x, props.y, props.width, props.height);
	};
</script>

<Layer {render} />
