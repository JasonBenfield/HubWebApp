
export class ModifierKey {
    readonly value: string;
    readonly displayText: string;

    constructor(source?: IModifierKey) {
        this.value = source ? source.Value : "";
        this.displayText = source ? source.DisplayText : "";
    }

    get isDefault() { return !this.value; }

    toString() { return this.displayText; }
}