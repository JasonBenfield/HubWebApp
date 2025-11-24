import { ModifierKey } from "./ModifierKey";

export class Modifier {
    readonly id: number;
    readonly categoryID: number;
    readonly modKey: ModifierKey;
    readonly targetKey: string;
    readonly displayText: string;

    constructor(source?: IModifierModel) {
        this.id = source ? source.ID : 0;
        this.categoryID = source ? source.CategoryID : 0;
        this.modKey = new ModifierKey(source && source.ModKey);
        this.targetKey = source ? source.TargetKey : "";
        this.displayText = source ? source.DisplayText : "";
    }

    get isDefault() { return this.modKey.isDefault; }
}