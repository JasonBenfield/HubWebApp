
export class AppVersionNumber {
	readonly major: number;
	readonly minor: number;
	readonly patch: number;

	constructor(source: IAppVersionNumber) {
		this.major = source.Major;
		this.minor = source.Minor;
		this.patch = source.Patch;
	}

	format() { return `${this.major}.${this.minor}.${this.patch}`; }

	toString() { return this.format(); }
}