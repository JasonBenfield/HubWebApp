// Generated code

import { AppApi } from "XtiShared/AppApi";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { UserGroup } from "./UserGroup";
import { UserAdminGroup } from "./UserAdminGroup";
import { AppsGroup } from "./AppsGroup";
import { AppGroup } from "./AppGroup";
import { ResourceGroupGroup } from "./ResourceGroupGroup";
import { ResourceGroup } from "./ResourceGroup";
import { ModCategoryGroup } from "./ModCategoryGroup";


export class HubAppApi extends AppApi {
	public static readonly DefaultVersion = 'V58';
	
	constructor(events: AppApiEvents, baseUrl: string, version: string = '') {
		super(events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion);
		this.User = this.addGroup((evts, resourceUrl) => new UserGroup(evts, resourceUrl));
		this.UserAdmin = this.addGroup((evts, resourceUrl) => new UserAdminGroup(evts, resourceUrl));
		this.Apps = this.addGroup((evts, resourceUrl) => new AppsGroup(evts, resourceUrl));
		this.App = this.addGroup((evts, resourceUrl) => new AppGroup(evts, resourceUrl));
		this.ResourceGroup = this.addGroup((evts, resourceUrl) => new ResourceGroupGroup(evts, resourceUrl));
		this.Resource = this.addGroup((evts, resourceUrl) => new ResourceGroup(evts, resourceUrl));
		this.ModCategory = this.addGroup((evts, resourceUrl) => new ModCategoryGroup(evts, resourceUrl));
	}
	
	readonly User: UserGroup;
	readonly UserAdmin: UserAdminGroup;
	readonly Apps: AppsGroup;
	readonly App: AppGroup;
	readonly ResourceGroup: ResourceGroupGroup;
	readonly Resource: ResourceGroup;
	readonly ModCategory: ModCategoryGroup;
}