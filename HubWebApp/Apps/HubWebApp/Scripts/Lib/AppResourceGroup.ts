import { AppResourceGroupName } from "./AppResourceGroupName";

export class AppResourceGroup {
	readonly id: number;
	readonly modCategoryID: number;
	readonly name: AppResourceGroupName;
	readonly isAnonymousAllowed: boolean;

	constructor(source: IResourceGroupModel) {
		this.id = source.ID;
		this.modCategoryID = source.ModCategoryID;
		this.name = new AppResourceGroupName(source.Name);
		this.isAnonymousAllowed = source.IsAnonymousAllowed;
    }
}