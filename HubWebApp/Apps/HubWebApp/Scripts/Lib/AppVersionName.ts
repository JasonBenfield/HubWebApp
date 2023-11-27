
export class AppVersionName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IAppVersionName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}