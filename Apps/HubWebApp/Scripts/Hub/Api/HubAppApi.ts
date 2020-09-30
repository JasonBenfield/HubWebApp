// Generated code

import { AppApi } from "../../Hub/AppApi";
import { AppApiEvents } from "../../Hub/AppApiEvents";
import { AuthGroup } from "./AuthGroup";
import { UserAdminGroup } from "./UserAdminGroup";

export class HubAppApi extends AppApi {
	constructor(events: AppApiEvents, baseUrl: string, version: string = 'V0') {
		super(events, baseUrl, 'Hub', version);
		this.Auth = this.addGroup((evts, resourceUrl) => new AuthGroup(evts, resourceUrl));
		this.UserAdmin = this.addGroup((evts, resourceUrl) => new UserAdminGroup(evts, resourceUrl));
	}

	readonly Auth: AuthGroup;
	readonly UserAdmin: UserAdminGroup;
}