// Generated code

import { AppApi } from "../../Hub/AppApi";
import { AppApiEvents } from "../../Hub/AppApiEvents";
import { AuthGroup } from "./AuthGroup";
import { UserAdminGroup } from "./UserAdminGroup";
import { RestrictedGroup } from "./RestrictedGroup";

export class HubAppApi extends AppApi {
	constructor(events: AppApiEvents, baseUrl: string) {
		super(events, baseUrl, 'Hub');
		this.Auth = this.addGroup((evts, resourceUrl) => new AuthGroup(evts, resourceUrl));
		this.UserAdmin = this.addGroup((evts, resourceUrl) => new UserAdminGroup(evts, resourceUrl));
		this.Restricted = this.addGroup((evts, resourceUrl) => new RestrictedGroup(evts, resourceUrl));
	}

	readonly Auth: AuthGroup;
	readonly UserAdmin: UserAdminGroup;
	readonly Restricted: RestrictedGroup;
}