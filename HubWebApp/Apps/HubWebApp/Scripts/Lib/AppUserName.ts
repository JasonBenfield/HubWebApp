
export class AppUserName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IAppUserName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}