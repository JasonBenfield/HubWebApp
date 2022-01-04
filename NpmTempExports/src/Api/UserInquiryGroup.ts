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
		this.GetCurrentUserAction = this.createAction<IEmptyRequest,IAppUserModel>('GetCurrentUser', 'Get Current User');
		this.RedirectToAppUser = this.createView<IRedirectToAppUserRequest>('RedirectToAppUser');
	}
	
	readonly GetUserAction: AppApiAction<number,IAppUserModel>;
	readonly GetUserByUserNameAction: AppApiAction<string,IAppUserModel>;
	readonly GetCurrentUserAction: AppApiAction<IEmptyRequest,IAppUserModel>;
	readonly RedirectToAppUser: AppApiView<IRedirectToAppUserRequest>;
	
	GetUser(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute(model, errorOptions || {});
	}
	GetUserByUserName(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetUserByUserNameAction.execute(model, errorOptions || {});
	}
	GetCurrentUser(errorOptions?: IActionErrorOptions) {
		return this.GetCurrentUserAction.execute({}, errorOptions || {});
	}
}