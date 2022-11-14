// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class CurrentUserGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'CurrentUser');
		this.GetUserAction = this.createAction<IEmptyRequest,IAppUserModel>('GetUser', 'Get User');
		this.Index = this.createView<IEmptyRequest>('Index');
	}
	
	readonly GetUserAction: AppApiAction<IEmptyRequest,IAppUserModel>;
	readonly Index: AppApiView<IEmptyRequest>;
	
	GetUser(errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute({}, errorOptions || {});
	}
}