
export class AppVersionNumber {
    readonly major: number;
    readonly minor: number;
    readonly patch: number;

    constructor(source?: IAppVersionNumber) {
        this.major = source ? source.Major : 0;
        this.minor = source ? source.Minor : 0;
        this.patch = source ? source.Patch : 0;
    }

    format() { return `${this.major}.${this.minor}.${this.patch}`; }

    toString() { return this.format(); }
}