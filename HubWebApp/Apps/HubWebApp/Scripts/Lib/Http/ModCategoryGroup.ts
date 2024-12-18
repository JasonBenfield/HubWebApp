// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class ModCategoryGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ModCategory');
		this.GetModCategoryAction = this.createAction<number,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetModifiersAction = this.createAction<number,IModifierModel[]>('GetModifiers', 'Get Modifiers');
		this.GetResourceGroupsAction = this.createAction<number,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
	}
	
	readonly GetModCategoryAction: AppClientAction<number,IModifierCategoryModel>;
	readonly GetModifiersAction: AppClientAction<number,IModifierModel[]>;
	readonly GetResourceGroupsAction: AppClientAction<number,IResourceGroupModel[]>;
	
	GetModCategory(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetModCategoryAction.execute(requestData, errorOptions || {});
	}
	GetModifiers(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetModifiersAction.execute(requestData, errorOptions || {});
	}
	GetResourceGroups(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupsAction.execute(requestData, errorOptions || {});
	}
}