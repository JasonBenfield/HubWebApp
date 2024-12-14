// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class UserInquiryGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserInquiry');
		this.GetUserAction = this.createAction<IAppUserIDRequest,IAppUserModel>('GetUser', 'Get User');
		this.GetUserAuthenticatorsAction = this.createAction<IAppUserIDRequest,IUserAuthenticatorModel[]>('GetUserAuthenticators', 'Get User Authenticators');
		this.GetUserOrAnonAction = this.createAction<IAppUserNameRequest,IAppUserModel>('GetUserOrAnon', 'Get User Or Anon');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
	}
	
	readonly GetUserAction: AppClientAction<IAppUserIDRequest,IAppUserModel>;
	readonly GetUserAuthenticatorsAction: AppClientAction<IAppUserIDRequest,IUserAuthenticatorModel[]>;
	readonly GetUserOrAnonAction: AppClientAction<IAppUserNameRequest,IAppUserModel>;
	readonly GetUsersAction: AppClientAction<IEmptyRequest,IAppUserModel[]>;
	
	GetUser(model: IAppUserIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute(model, errorOptions || {});
	}
	GetUserAuthenticators(model: IAppUserIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserAuthenticatorsAction.execute(model, errorOptions || {});
	}
	GetUserOrAnon(model: IAppUserNameRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserOrAnonAction.execute(model, errorOptions || {});
	}
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
}