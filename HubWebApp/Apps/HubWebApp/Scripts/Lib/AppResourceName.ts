
export class AppResourceName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IResourceName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}