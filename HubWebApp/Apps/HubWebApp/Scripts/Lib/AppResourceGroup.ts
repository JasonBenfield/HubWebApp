import { AppResourceGroupName } from "./AppResourceGroupName";

export class AppResourceGroup {
    readonly id: number;
    readonly modCategoryID: number;
    readonly name: AppResourceGroupName;
    readonly isAnonymousAllowed: boolean;

    constructor(source?: IResourceGroupModel) {
        this.id = source ? source.ID : 0;
        this.modCategoryID = source ? source.ModCategoryID : 0;
        this.name = new AppResourceGroupName(source && source.Name);
        this.isAnonymousAllowed = source ? source.IsAnonymousAllowed : false;
    }
}