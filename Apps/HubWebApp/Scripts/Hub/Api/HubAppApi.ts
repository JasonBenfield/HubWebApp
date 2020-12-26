// Generated code

import { AppApi } from "../../Shared/AppApi";
import { AppApiEvents } from "../../Shared/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { UserAdminGroup } from "./UserAdminGroup";
import { AppsGroup } from "./AppsGroup";
import { AppGroup } from "./AppGroup";

export class HubAppApi extends AppApi {
	public static readonly DefaultVersion = 'V58';

	constructor(events: AppApiEvents, baseUrl: string, version: string = '') {
		super(events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion);
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserAdmin = this.addGroup((evts, resourceUrl) => new UserAdminGroup(evts, resourceUrl));
		this.Apps = this.addGroup((evts, resourceUrl) => new AppsGroup(evts, resourceUrl));
		this.App = this.addGroup((evts, resourceUrl) => new AppGroup(evts, resourceUrl));
	}

	readonly User: UserGroup;
	readonly UserAdmin: UserAdminGroup;
	readonly Apps: AppsGroup;
	readonly App: AppGroup;
}