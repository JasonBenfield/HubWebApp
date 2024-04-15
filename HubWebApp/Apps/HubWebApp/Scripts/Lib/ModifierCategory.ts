import { ModifierCategoryName } from "./ModifierCategoryName";

export class ModifierCategory {
    readonly id: number;
    readonly name: ModifierCategoryName;

    constructor(readonly source: IModifierCategoryModel) {
        this.id = source.ID;
        this.name = new ModifierCategoryName(source.Name);
    }

    get isDefault() { return this.name.isDefault; }

}