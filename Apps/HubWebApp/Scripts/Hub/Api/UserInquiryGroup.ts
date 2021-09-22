// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

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