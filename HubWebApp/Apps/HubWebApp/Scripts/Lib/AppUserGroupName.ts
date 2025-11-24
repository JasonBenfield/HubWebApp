
export class AppUserGroupName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IAppUserGroupName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}