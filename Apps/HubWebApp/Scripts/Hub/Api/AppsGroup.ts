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
		this.AllAction = this.createAction<IEmptyRequest,IAppModel[]>('All', 'All');
		this.GetAppModifierKeyAction = this.createAction<IAppKey,string>('GetAppModifierKey', 'Get App Modifier Key');
		this.RedirectToApp = this.createView<number>('RedirectToApp');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly AllAction: AppApiAction<IEmptyRequest,IAppModel[]>;
	readonly GetAppModifierKeyAction: AppApiAction<IAppKey,string>;
	readonly RedirectToApp: AppApiView<number>;
	
	All(errorOptions?: IActionErrorOptions) {
		return this.AllAction.execute({}, errorOptions || {});
	}
	GetAppModifierKey(model: IAppKey, errorOptions?: IActionErrorOptions) {
		return this.GetAppModifierKeyAction.execute(model, errorOptions || {});
	}
}