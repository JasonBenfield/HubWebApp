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
		this.GetUserByUserNameAction = this.createAction<string,IAppUserModel>('GetUserByUserName', 'Get User By User Name');
	}
	
	readonly GetUserAction: AppApiAction<number,IAppUserModel>;
	readonly GetUserByUserNameAction: AppApiAction<string,IAppUserModel>;
	
	GetUser(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute(model, errorOptions || {});
	}
	GetUserByUserName(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetUserByUserNameAction.execute(model, errorOptions || {});
	}
}