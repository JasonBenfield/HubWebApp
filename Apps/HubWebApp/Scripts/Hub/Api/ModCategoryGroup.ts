// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class ModCategoryGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ModCategory');
		this.GetModCategoryAction = this.createAction<number,IModifierCategoryModel>('GetModCategory', 'Get Mod Category');
		this.GetModifiersAction = this.createAction<number,IModifierModel[]>('GetModifiers', 'Get Modifiers');
		this.GetModifierAction = this.createAction<IGetModCategoryModifierRequest,IModifierModel>('GetModifier', 'Get Modifier');
		this.GetResourceGroupsAction = this.createAction<number,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
	}
	
	readonly GetModCategoryAction: AppApiAction<number,IModifierCategoryModel>;
	readonly GetModifiersAction: AppApiAction<number,IModifierModel[]>;
	readonly GetModifierAction: AppApiAction<IGetModCategoryModifierRequest,IModifierModel>;
	readonly GetResourceGroupsAction: AppApiAction<number,IResourceGroupModel[]>;
	
	GetModCategory(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetModCategoryAction.execute(model, errorOptions || {});
	}
	GetModifiers(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetModifiersAction.execute(model, errorOptions || {});
	}
	GetModifier(model: IGetModCategoryModifierRequest, errorOptions?: IActionErrorOptions) {
		return this.GetModifierAction.execute(model, errorOptions || {});
	}
	GetResourceGroups(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupsAction.execute(model, errorOptions || {});
	}
}