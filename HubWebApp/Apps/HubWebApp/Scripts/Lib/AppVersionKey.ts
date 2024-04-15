
export class AppVersionKey {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IAppVersionKey) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}