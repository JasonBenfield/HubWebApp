// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AuthenticatorsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Authenticators');
		this.MoveAuthenticatorAction = this.createAction<IMoveAuthenticatorRequest,IEmptyActionResult>('MoveAuthenticator', 'Move Authenticator');
		this.RegisterAuthenticatorAction = this.createAction<IRegisterAuthenticatorRequest,IAuthenticatorModel>('RegisterAuthenticator', 'Register Authenticator');
		this.RegisterUserAuthenticatorAction = this.createAction<IRegisterUserAuthenticatorRequest,IAuthenticatorModel>('RegisterUserAuthenticator', 'Register User Authenticator');
		this.UserOrAnonByAuthenticatorAction = this.createAction<IUserOrAnonByAuthenticatorRequest,IAppUserModel>('UserOrAnonByAuthenticator', 'User Or Anon By Authenticator');
	}
	
	readonly MoveAuthenticatorAction: AppApiAction<IMoveAuthenticatorRequest,IEmptyActionResult>;
	readonly RegisterAuthenticatorAction: AppApiAction<IRegisterAuthenticatorRequest,IAuthenticatorModel>;
	readonly RegisterUserAuthenticatorAction: AppApiAction<IRegisterUserAuthenticatorRequest,IAuthenticatorModel>;
	readonly UserOrAnonByAuthenticatorAction: AppApiAction<IUserOrAnonByAuthenticatorRequest,IAppUserModel>;
	
	MoveAuthenticator(model: IMoveAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.MoveAuthenticatorAction.execute(model, errorOptions || {});
	}
	RegisterAuthenticator(model: IRegisterAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterAuthenticatorAction.execute(model, errorOptions || {});
	}
	RegisterUserAuthenticator(model: IRegisterUserAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterUserAuthenticatorAction.execute(model, errorOptions || {});
	}
	UserOrAnonByAuthenticator(model: IUserOrAnonByAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.UserOrAnonByAuthenticatorAction.execute(model, errorOptions || {});
	}
}