
export class ModifierCategoryName {
    readonly value: string;
    readonly displayText: string;

    constructor(source: IModifierCategoryName) {
        this.value = source.Value;
        this.displayText = source.DisplayText;
    }

    get isDefault() { return this.value.toLowerCase() === 'default'; }

    toString() { return this.displayText; }
}