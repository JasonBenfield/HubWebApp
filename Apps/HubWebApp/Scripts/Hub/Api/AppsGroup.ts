// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

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