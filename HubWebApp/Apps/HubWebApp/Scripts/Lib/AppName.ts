
export class AppName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IAppName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}