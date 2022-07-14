// Generated code

import { AppApi } from "@jasonbenfield/sharedwebapp/Api/AppApi";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppApiQuery } from "@jasonbenfield/sharedwebapp/Api/AppApiQuery";
import { UserGroup } from "./UserGroup";
import { UserCacheGroup } from "./UserCacheGroup";
import { HomeGroup } from "./HomeGroup";


export class AuthenticatorAppApi extends AppApi {
	constructor(events: AppApiEvents) {
		super(events, 'Authenticator');
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserCache = this.addGroup((evts, resourceUrl) => new UserCacheGroup(evts, resourceUrl));
		this.Home = this.addGroup((evts, resourceUrl) => new HomeGroup(evts, resourceUrl));
	}
	
	readonly User: UserGroup;
	readonly UserCache: UserCacheGroup;
	readonly Home: HomeGroup;
}