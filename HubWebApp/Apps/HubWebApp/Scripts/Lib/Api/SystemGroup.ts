// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class SystemGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'System');
		this.GetAppContextAction = this.createAction<IGetAppContextRequest,IAppContextModel>('GetAppContext', 'Get App Context');
		this.GetUserContextAction = this.createAction<IGetUserContextRequest,IUserContextModel>('GetUserContext', 'Get User Context');
		this.AddOrUpdateModifierByTargetKeyAction = this.createAction<IAddOrUpdateModifierByTargetKeyRequest,IModifierModel>('AddOrUpdateModifierByTargetKey', 'Add Or Update Modifier By Target Key');
	}
	
	readonly GetAppContextAction: AppApiAction<IGetAppContextRequest,IAppContextModel>;
	readonly GetUserContextAction: AppApiAction<IGetUserContextRequest,IUserContextModel>;
	readonly AddOrUpdateModifierByTargetKeyAction: AppApiAction<IAddOrUpdateModifierByTargetKeyRequest,IModifierModel>;
	
	GetAppContext(model: IGetAppContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppContextAction.execute(model, errorOptions || {});
	}
	GetUserContext(model: IGetUserContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserContextAction.execute(model, errorOptions || {});
	}
	AddOrUpdateModifierByTargetKey(model: IAddOrUpdateModifierByTargetKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByTargetKeyAction.execute(model, errorOptions || {});
	}
}