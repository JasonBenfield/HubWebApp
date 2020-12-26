// Generated code

import { AppApiGroup } from "../../Shared/AppApiGroup";
import { AppApiAction } from "../../Shared/AppApiAction";
import { AppApiView } from "../../Shared/AppApiView";
import { AppApiEvents } from "../../Shared/AppApiEvents";
import { AppResourceUrl } from "../../Shared/AppResourceUrl";

export class AppsGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Apps');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.AllAction = this.createAction<IEmptyRequest,IAppModel[]>('All', 'All');
		this.RedirectToApp = this.createView<number>('RedirectToApp');
	}

	readonly Index: AppApiView<IEmptyRequest>;
	private readonly AllAction: AppApiAction<IEmptyRequest,IAppModel[]>;
	readonly RedirectToApp: AppApiView<number>;

	All(errorOptions?: IActionErrorOptions) {
		return this.AllAction.execute({}, errorOptions || {});
	}
}