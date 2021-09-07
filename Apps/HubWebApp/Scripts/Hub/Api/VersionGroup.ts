// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class VersionGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Version');
		this.GetVersionAction = this.createAction<string,IAppVersionModel>('GetVersion', 'Get Version');
		this.GetCurrentVersionAction = this.createAction<IEmptyRequest,IAppVersionModel>('GetCurrentVersion', 'Get Current Version');
		this.GetResourceGroupAction = this.createAction<IGetVersionResourceGroupRequest,IResourceGroupModel>('GetResourceGroup', 'Get Resource Group');
	}
	
	readonly GetVersionAction: AppApiAction<string,IAppVersionModel>;
	readonly GetCurrentVersionAction: AppApiAction<IEmptyRequest,IAppVersionModel>;
	readonly GetResourceGroupAction: AppApiAction<IGetVersionResourceGroupRequest,IResourceGroupModel>;
	
	GetVersion(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetVersionAction.execute(model, errorOptions || {});
	}
	GetCurrentVersion(errorOptions?: IActionErrorOptions) {
		return this.GetCurrentVersionAction.execute({}, errorOptions || {});
	}
	GetResourceGroup(model: IGetVersionResourceGroupRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupAction.execute(model, errorOptions || {});
	}
}