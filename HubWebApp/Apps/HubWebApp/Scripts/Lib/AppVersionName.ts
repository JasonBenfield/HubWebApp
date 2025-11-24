
export class AppVersionName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppVersionName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}