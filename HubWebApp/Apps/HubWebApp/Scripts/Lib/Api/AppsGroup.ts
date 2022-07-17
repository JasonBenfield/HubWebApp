// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class AppsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Apps');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetAppsAction = this.createAction<IEmptyRequest,IAppModel[]>('GetApps', 'Get Apps');
		this.GetAppDomainsAction = this.createAction<IEmptyRequest,IAppDomainModel[]>('GetAppDomains', 'Get App Domains');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetAppsAction: AppApiAction<IEmptyRequest,IAppModel[]>;
	readonly GetAppDomainsAction: AppApiAction<IEmptyRequest,IAppDomainModel[]>;
	
	GetApps(errorOptions?: IActionErrorOptions) {
		return this.GetAppsAction.execute({}, errorOptions || {});
	}
	GetAppDomains(errorOptions?: IActionErrorOptions) {
		return this.GetAppDomainsAction.execute({}, errorOptions || {});
	}
}