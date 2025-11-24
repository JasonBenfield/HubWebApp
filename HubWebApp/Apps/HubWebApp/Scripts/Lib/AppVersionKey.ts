
export class AppVersionKey {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppVersionKey) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}