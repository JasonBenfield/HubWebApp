// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class VersionGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Version');
		this.GetVersionAction = this.createAction<string,IAppVersionModel>('GetVersion', 'Get Version');
		this.GetResourceGroupAction = this.createAction<IGetVersionResourceGroupRequest,IResourceGroupModel>('GetResourceGroup', 'Get Resource Group');
	}
	
	readonly GetVersionAction: AppApiAction<string,IAppVersionModel>;
	readonly GetResourceGroupAction: AppApiAction<IGetVersionResourceGroupRequest,IResourceGroupModel>;
	
	GetVersion(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetVersionAction.execute(model, errorOptions || {});
	}
	GetResourceGroup(model: IGetVersionResourceGroupRequest, errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupAction.execute(model, errorOptions || {});
	}
}