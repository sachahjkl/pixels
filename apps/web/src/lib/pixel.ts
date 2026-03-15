export type Color = `#${string}`;

export class Pixel {
	constructor(
		private x: number,
		private y: number,
		private _color: Color
	) {}

	public set color(color: Color) {
		this._color = color;
	}

	public get color() {
		return this._color;
	}
}
