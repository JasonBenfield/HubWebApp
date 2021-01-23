// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class AppsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Apps');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.AllAction = this.createAction<IEmptyRequest,IAppModel[]>('All', 'All');
		this.RedirectToApp = this.createView<number>('RedirectToApp');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly AllAction: AppApiAction<IEmptyRequest,IAppModel[]>;
	readonly RedirectToApp: AppApiView<number>;
	
	All(errorOptions?: IActionErrorOptions) {
		return this.AllAction.execute({}, errorOptions || {});
	}
}