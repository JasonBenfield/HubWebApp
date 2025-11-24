
export class ModifierCategoryName {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IModifierCategoryName) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    get isDefault() { return this.value.toLowerCase() === "default"; }

    toString() { return this.displayText; }
}