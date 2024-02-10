
export class AppUserGroupName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IAppUserGroupName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}