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
		this.GetUserAction = this.createAction<number,IAppUserModel>('GetUser', 'Get User');
		this.GetUserOrAnonAction = this.createAction<string,IAppUserModel>('GetUserOrAnon', 'Get User Or Anon');
		this.GetUserAuthenticatorsAction = this.createAction<number,IUserAuthenticatorModel[]>('GetUserAuthenticators', 'Get User Authenticators');
		this.GetUsersAction = this.createAction<IEmptyRequest,IAppUserModel[]>('GetUsers', 'Get Users');
	}
	
	readonly GetUserAction: AppClientAction<number,IAppUserModel>;
	readonly GetUserOrAnonAction: AppClientAction<string,IAppUserModel>;
	readonly GetUserAuthenticatorsAction: AppClientAction<number,IUserAuthenticatorModel[]>;
	readonly GetUsersAction: AppClientAction<IEmptyRequest,IAppUserModel[]>;
	
	GetUser(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserAction.execute(model, errorOptions || {});
	}
	GetUserOrAnon(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetUserOrAnonAction.execute(model, errorOptions || {});
	}
	GetUserAuthenticators(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserAuthenticatorsAction.execute(model, errorOptions || {});
	}
	GetUsers(errorOptions?: IActionErrorOptions) {
		return this.GetUsersAction.execute({}, errorOptions || {});
	}
}