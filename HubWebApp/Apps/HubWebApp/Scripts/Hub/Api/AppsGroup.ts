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
		this.GetAppDomainAction = this.createAction<IGetAppDomainRequest,string>('GetAppDomain', 'Get App Domain');
		this.GetAppsAction = this.createAction<IEmptyRequest,IAppWithModKeyModel[]>('GetApps', 'Get Apps');
		this.GetAppByIdAction = this.createAction<IGetAppByIDRequest,IAppWithModKeyModel>('GetAppById', 'Get App By Id');
		this.GetAppByAppKeyAction = this.createAction<IGetAppByAppKeyRequest,IAppWithModKeyModel>('GetAppByAppKey', 'Get App By App Key');
		this.RedirectToApp = this.createView<number>('RedirectToApp');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly GetAppDomainAction: AppApiAction<IGetAppDomainRequest,string>;
	readonly GetAppsAction: AppApiAction<IEmptyRequest,IAppWithModKeyModel[]>;
	readonly GetAppByIdAction: AppApiAction<IGetAppByIDRequest,IAppWithModKeyModel>;
	readonly GetAppByAppKeyAction: AppApiAction<IGetAppByAppKeyRequest,IAppWithModKeyModel>;
	readonly RedirectToApp: AppApiView<number>;
	
	GetAppDomain(model: IGetAppDomainRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppDomainAction.execute(model, errorOptions || {});
	}
	GetApps(errorOptions?: IActionErrorOptions) {
		return this.GetAppsAction.execute({}, errorOptions || {});
	}
	GetAppById(model: IGetAppByIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppByIdAction.execute(model, errorOptions || {});
	}
	GetAppByAppKey(model: IGetAppByAppKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppByAppKeyAction.execute(model, errorOptions || {});
	}
}