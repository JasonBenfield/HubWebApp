
export class AppName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}