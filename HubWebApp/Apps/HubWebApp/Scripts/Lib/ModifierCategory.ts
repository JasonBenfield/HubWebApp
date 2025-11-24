import { ModifierCategoryName } from "./ModifierCategoryName";

export class ModifierCategory {
    readonly id: number;
    readonly name: ModifierCategoryName;

    constructor(source?: IModifierCategoryModel) {
        this.id = source ? source.ID : 0;
        this.name = new ModifierCategoryName(source && source.Name);
    }

    get isDefault() { return this.name.isDefault; }

}