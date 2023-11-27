
export class AppResourceName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IResourceName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}