// Generated code

import { AppClient, IGetUserAccessRequest, AppClientMethod } from "@jasonbenfield/sharedwebapp/Http/AppClient";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppClientQuery } from "@jasonbenfield/sharedwebapp/Http/AppClientQuery";
import { HomeGroup } from "./HomeGroup";


export class AuthenticatorAppClient extends AppClient {
	constructor(events: AppClientEvents) {
		super(
			events, 
			'Authenticator', 
			pageContext.EnvironmentName === 'Production' || pageContext.EnvironmentName === 'Staging' ? 'V1435' : 'Current'
		);
		this.Home = this.addGroup((evts, resourceUrl) => new HomeGroup(evts, resourceUrl));
	}
	
	readonly Home: HomeGroup;
	
	getAccessRequest(getAction: (api: AuthenticatorAppClient) => AppClientMethod, modKey?: string) { return this._getAccessRequest(getAction, modKey); }
	
	getUserAccess(resources: IGetUserAccessRequest<AuthenticatorAppClient>) { return this._getUserAccess(resources); }
}