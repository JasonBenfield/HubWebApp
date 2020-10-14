// Generated code

import { AppApi } from "../../Hub/AppApi";
import { AppApiEvents } from "../../Hub/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { AuthGroup } from "./AuthGroup";
import { AuthApiGroup } from "./AuthApiGroup";
import { UserAdminGroup } from "./UserAdminGroup";

export class HubAppApi extends AppApi {
	constructor(events: AppApiEvents, baseUrl: string, version: string = 'V2') {
		super(events, baseUrl, 'Hub', version);
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.Auth = this.addGroup((evts, resourceUrl) => new AuthGroup(evts, resourceUrl));
		this.AuthApi = this.addGroup((evts, resourceUrl) => new AuthApiGroup(evts, resourceUrl));
		this.UserAdmin = this.addGroup((evts, resourceUrl) => new UserAdminGroup(evts, resourceUrl));
	}

	readonly User: UserGroup;
	readonly Auth: AuthGroup;
	readonly AuthApi: AuthApiGroup;
	readonly UserAdmin: UserAdminGroup;
}