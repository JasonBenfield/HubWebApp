
export class AppRoleName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IAppRoleName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}