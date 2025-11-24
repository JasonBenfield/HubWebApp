
export class AppResourceGroupName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IResourceGroupName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    toString() { return this.displayText; }
}