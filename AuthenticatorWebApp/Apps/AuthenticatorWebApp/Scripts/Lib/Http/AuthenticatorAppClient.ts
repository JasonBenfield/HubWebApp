// Generated code

import { AppClient } from "@jasonbenfield/sharedwebapp/Http/AppClient";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppClientQuery } from "@jasonbenfield/sharedwebapp/Http/AppClientQuery";
import { HomeGroup } from "./HomeGroup";


export class AuthenticatorAppClient extends AppClient {
	constructor(events: AppClientEvents) {
		super(events, 'Authenticator');
		this.Home = this.addGroup((evts, resourceUrl) => new HomeGroup(evts, resourceUrl));
	}
	
	readonly Home: HomeGroup;
}