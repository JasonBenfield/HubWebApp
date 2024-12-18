// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AuthenticatorsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Authenticators');
		this.MoveAuthenticatorAction = this.createAction<IMoveAuthenticatorRequest,IEmptyActionResult>('MoveAuthenticator', 'Move Authenticator');
		this.RegisterAuthenticatorAction = this.createAction<IRegisterAuthenticatorRequest,IAuthenticatorModel>('RegisterAuthenticator', 'Register Authenticator');
		this.RegisterUserAuthenticatorAction = this.createAction<IRegisterUserAuthenticatorRequest,IAuthenticatorModel>('RegisterUserAuthenticator', 'Register User Authenticator');
		this.UserOrAnonByAuthenticatorAction = this.createAction<IUserOrAnonByAuthenticatorRequest,IAppUserModel>('UserOrAnonByAuthenticator', 'User Or Anon By Authenticator');
	}
	
	readonly MoveAuthenticatorAction: AppClientAction<IMoveAuthenticatorRequest,IEmptyActionResult>;
	readonly RegisterAuthenticatorAction: AppClientAction<IRegisterAuthenticatorRequest,IAuthenticatorModel>;
	readonly RegisterUserAuthenticatorAction: AppClientAction<IRegisterUserAuthenticatorRequest,IAuthenticatorModel>;
	readonly UserOrAnonByAuthenticatorAction: AppClientAction<IUserOrAnonByAuthenticatorRequest,IAppUserModel>;
	
	MoveAuthenticator(requestData: IMoveAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.MoveAuthenticatorAction.execute(requestData, errorOptions || {});
	}
	RegisterAuthenticator(requestData: IRegisterAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterAuthenticatorAction.execute(requestData, errorOptions || {});
	}
	RegisterUserAuthenticator(requestData: IRegisterUserAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterUserAuthenticatorAction.execute(requestData, errorOptions || {});
	}
	UserOrAnonByAuthenticator(requestData: IUserOrAnonByAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.UserOrAnonByAuthenticatorAction.execute(requestData, errorOptions || {});
	}
}