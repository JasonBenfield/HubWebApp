// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class ExternalAuthGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ExternalAuth');
		this.ExternalAuthKeyAction = this.createAction<IExternalAuthKeyModel,string>('ExternalAuthKey', 'External Auth Key');
	}
	
	readonly ExternalAuthKeyAction: AppApiAction<IExternalAuthKeyModel,string>;
	
	ExternalAuthKey(model: IExternalAuthKeyModel, errorOptions?: IActionErrorOptions) {
		return this.ExternalAuthKeyAction.execute(model, errorOptions || {});
	}
}