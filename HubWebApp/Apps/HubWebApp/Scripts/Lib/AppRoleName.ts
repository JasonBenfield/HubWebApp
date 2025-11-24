
export class AppRoleName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppRoleName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}