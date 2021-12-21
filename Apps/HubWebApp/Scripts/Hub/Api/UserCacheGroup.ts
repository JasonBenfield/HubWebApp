// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class UserCacheGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserCache');
		this.ClearCacheAction = this.createAction<string,IEmptyActionResult>('ClearCache', 'Clear Cache');
	}
	
	readonly ClearCacheAction: AppApiAction<string,IEmptyActionResult>;
	
	ClearCache(model: string, errorOptions?: IActionErrorOptions) {
		return this.ClearCacheAction.execute(model, errorOptions || {});
	}
}