
export class ModifierKey {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IModifierKey) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    get isDefault() { return !this.value; }

    toString() { return this.displayText; }
}