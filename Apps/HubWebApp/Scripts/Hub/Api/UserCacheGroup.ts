// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

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