// Generated code

import { AppApi } from "../../Shared/AppApi";
import { AppApiEvents } from "../../Shared/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { UserAdminGroup } from "./UserAdminGroup";

export class HubAppApi extends AppApi {
	public static readonly DefaultVersion = 'Current';

	constructor(events: AppApiEvents, baseUrl: string, version: string = '') {
		super(events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion);
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserAdmin = this.addGroup((evts, resourceUrl) => new UserAdminGroup(evts, resourceUrl));
	}

	readonly User: UserGroup;
	readonly UserAdmin: UserAdminGroup;
}