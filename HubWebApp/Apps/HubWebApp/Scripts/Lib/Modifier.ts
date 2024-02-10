import { ModifierKey } from "./ModifierKey";

export class Modifier {
	readonly id: number;
	readonly categoryID: number;
	readonly modKey: ModifierKey;
	readonly targetKey: string;
	readonly displayText: string;

	constructor(readonly source: IModifierModel) {
		this.id = source.ID;
		this.categoryID = source.CategoryID;
		this.modKey = new ModifierKey(source.ModKey);
		this.targetKey = source.TargetKey;
		this.displayText = source.DisplayText;
	}

	get isDefault() { return this.modKey.isDefault; }
}