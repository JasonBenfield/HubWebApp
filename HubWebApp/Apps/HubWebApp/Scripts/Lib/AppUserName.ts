
export class AppUserName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppUserName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}