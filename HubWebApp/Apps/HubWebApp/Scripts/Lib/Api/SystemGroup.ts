// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class SystemGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'System');
		this.GetAppContextAction = this.createAction<IEmptyRequest,IAppContextModel>('GetAppContext', 'Get App Context');
		this.GetUserContextAction = this.createAction<IGetUserContextRequest,IUserContextModel>('GetUserContext', 'Get User Context');
	}
	
	readonly GetAppContextAction: AppApiAction<IEmptyRequest,IAppContextModel>;
	readonly GetUserContextAction: AppApiAction<IGetUserContextRequest,IUserContextModel>;
	
	GetAppContext(errorOptions?: IActionErrorOptions) {
		return this.GetAppContextAction.execute({}, errorOptions || {});
	}
	GetUserContext(model: IGetUserContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserContextAction.execute(model, errorOptions || {});
	}
}