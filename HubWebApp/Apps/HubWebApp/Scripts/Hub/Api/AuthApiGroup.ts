// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AuthApiGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AuthApi');
		this.AuthenticateAction = this.createAction<ILoginCredentials,ILoginResult>('Authenticate', 'Authenticate');
		this.LogoutAction = this.createAction<IEmptyRequest,IEmptyActionResult>('Logout', 'Logout');
	}
	
	readonly AuthenticateAction: AppApiAction<ILoginCredentials,ILoginResult>;
	readonly LogoutAction: AppApiAction<IEmptyRequest,IEmptyActionResult>;
	
	Authenticate(model: ILoginCredentials, errorOptions?: IActionErrorOptions) {
		return this.AuthenticateAction.execute(model, errorOptions || {});
	}
	Logout(errorOptions?: IActionErrorOptions) {
		return this.LogoutAction.execute({}, errorOptions || {});
	}
}