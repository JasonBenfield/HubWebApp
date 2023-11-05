// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppsGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Apps');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.GetAppsAction = this.createAction<IEmptyRequest,IAppModel[]>('GetApps', 'Get Apps');
		this.GetAppDomainsAction = this.createAction<IEmptyRequest,IAppDomainModel[]>('GetAppDomains', 'Get App Domains');
	}
	
	readonly Index: AppClientView<IEmptyRequest>;
	readonly GetAppsAction: AppClientAction<IEmptyRequest,IAppModel[]>;
	readonly GetAppDomainsAction: AppClientAction<IEmptyRequest,IAppDomainModel[]>;
	
	GetApps(errorOptions?: IActionErrorOptions) {
		return this.GetAppsAction.execute({}, errorOptions || {});
	}
	GetAppDomains(errorOptions?: IActionErrorOptions) {
		return this.GetAppDomainsAction.execute({}, errorOptions || {});
	}
}