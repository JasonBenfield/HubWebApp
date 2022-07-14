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
		this.GetAppsAction = this.createAction<IEmptyRequest,IAppWithModKeyModel[]>('GetApps', 'Get Apps');
		this.GetAppByIDAction = this.createAction<IGetAppByIDRequest,IAppWithModKeyModel>('GetAppByID', 'Get App By ID');
		this.GetAppByAppKeyAction = this.createAction<IGetAppByAppKeyRequest,IAppWithModKeyModel>('GetAppByAppKey', 'Get App By App Key');
		this.RedirectToApp = this.createView<number>('RedirectToApp');
		this.GetAppDomainsAction = this.createAction<IEmptyRequest,IAppDomainModel[]>('GetAppDomains', 'Get App Domains');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetAppsAction: AppApiAction<IEmptyRequest,IAppWithModKeyModel[]>;
	readonly GetAppByIDAction: AppApiAction<IGetAppByIDRequest,IAppWithModKeyModel>;
	readonly GetAppByAppKeyAction: AppApiAction<IGetAppByAppKeyRequest,IAppWithModKeyModel>;
	readonly RedirectToApp: AppApiView<number>;
	readonly GetAppDomainsAction: AppApiAction<IEmptyRequest,IAppDomainModel[]>;
	
	GetApps(errorOptions?: IActionErrorOptions) {
		return this.GetAppsAction.execute({}, errorOptions || {});
	}
	GetAppByID(model: IGetAppByIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppByIDAction.execute(model, errorOptions || {});
	}
	GetAppByAppKey(model: IGetAppByAppKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppByAppKeyAction.execute(model, errorOptions || {});
	}
	GetAppDomains(errorOptions?: IActionErrorOptions) {
		return this.GetAppDomainsAction.execute({}, errorOptions || {});
	}
}