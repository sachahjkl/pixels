export function randomColor(): `#${string}` {
	const n = (Math.random() * 0xffffff) | 0;
	return `#${n.toString(16).padStart(6, "0")}` as `#${string}`;
}
