// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class ModCategoryGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ModCategory');
		this.GetModCategoryAction = this.createAction<number,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetModifiersAction = this.createAction<number,IModifierModel[]>('GetModifiers', 'Get Modifiers');
		this.GetResourceGroupsAction = this.createAction<number,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
	}
	
	readonly GetModCategoryAction: AppApiAction<number,IModifierCategoryModel>;
	readonly GetModifiersAction: AppApiAction<number,IModifierModel[]>;
	readonly GetResourceGroupsAction: AppApiAction<number,IResourceGroupModel[]>;
	
	GetModCategory(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetModCategoryAction.execute(model, errorOptions || {});
	}
	GetModifiers(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetModifiersAction.execute(model, errorOptions || {});
	}
	GetResourceGroups(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupsAction.execute(model, errorOptions || {});
	}
}