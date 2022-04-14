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
	}
	
	readonly AuthenticateAction: AppApiAction<ILoginCredentials,ILoginResult>;
	
	Authenticate(model: ILoginCredentials, errorOptions?: IActionErrorOptions) {
		return this.AuthenticateAction.execute(model, errorOptions || {});
	}
}