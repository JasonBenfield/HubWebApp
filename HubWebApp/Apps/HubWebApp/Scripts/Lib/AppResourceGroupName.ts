
export class AppResourceGroupName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IResourceGroupName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    toString() { return this.displayText; }
}