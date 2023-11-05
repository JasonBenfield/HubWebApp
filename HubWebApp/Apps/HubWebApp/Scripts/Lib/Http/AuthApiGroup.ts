// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AuthApiGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AuthApi');
		this.AuthenticateAction = this.createAction<ILoginCredentials,ILoginResult>('Authenticate', 'Authenticate');
	}
	
	readonly AuthenticateAction: AppClientAction<ILoginCredentials,ILoginResult>;
	
	Authenticate(model: ILoginCredentials, errorOptions?: IActionErrorOptions) {
		return this.AuthenticateAction.execute(model, errorOptions || {});
	}
}