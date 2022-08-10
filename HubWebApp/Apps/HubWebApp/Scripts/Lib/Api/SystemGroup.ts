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
	}
	
	readonly GetAppContextAction: AppApiAction<IGetAppContextRequest,IAppContextModel>;
	readonly GetUserContextAction: AppApiAction<IGetUserContextRequest,IUserContextModel>;
	
	GetAppContext(model: IGetAppContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppContextAction.execute(model, errorOptions || {});
	}
	GetUserContext(model: IGetUserContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserContextAction.execute(model, errorOptions || {});
	}
}