// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AuthenticatorsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Authenticators');
		this.RegisterAuthenticatorAction = this.createAction<IEmptyRequest,IEmptyActionResult>('RegisterAuthenticator', 'Register Authenticator');
		this.RegisterUserAuthenticatorAction = this.createAction<IRegisterUserAuthenticatorRequest,IEmptyActionResult>('RegisterUserAuthenticator', 'Register User Authenticator');
	}
	
	readonly RegisterAuthenticatorAction: AppApiAction<IEmptyRequest,IEmptyActionResult>;
	readonly RegisterUserAuthenticatorAction: AppApiAction<IRegisterUserAuthenticatorRequest,IEmptyActionResult>;
	
	RegisterAuthenticator(errorOptions?: IActionErrorOptions) {
		return this.RegisterAuthenticatorAction.execute({}, errorOptions || {});
	}
	RegisterUserAuthenticator(model: IRegisterUserAuthenticatorRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterUserAuthenticatorAction.execute(model, errorOptions || {});
	}
}