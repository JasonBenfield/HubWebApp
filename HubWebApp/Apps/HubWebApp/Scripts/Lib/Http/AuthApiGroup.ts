// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AuthApiGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AuthApi');
		this.AuthenticateAction = this.createAction<IAuthenticateRequest,ILoginResult>('Authenticate', 'Authenticate');
	}
	
	readonly AuthenticateAction: AppClientAction<IAuthenticateRequest,ILoginResult>;
	
	Authenticate(requestData: IAuthenticateRequest, errorOptions?: IActionErrorOptions) {
		return this.AuthenticateAction.execute(requestData, errorOptions || {});
	}
}