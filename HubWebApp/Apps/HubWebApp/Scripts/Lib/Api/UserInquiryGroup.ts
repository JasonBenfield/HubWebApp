// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class UserInquiryGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserInquiry');
		this.GetUserAction = this.createAction<number,IAppUserModel>('GetUser', 'Get User');
		this.GetUserOrAnonAction = this.createAction<string,IAppUserModel>('GetUserOrAnon', 'Get User Or Anon');
	}
	
	readonly GetUserAction: AppApiAction<number,IAppUserModel>;
	readonly GetUserOrAnonAction: AppApiAction<string,IAppUserModel>;
	
	GetUser(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute(model, errorOptions || {});
	}
	GetUserOrAnon(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetUserOrAnonAction.execute(model, errorOptions || {});
	}
}