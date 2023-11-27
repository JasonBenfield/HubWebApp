// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class ExternalAuthGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'ExternalAuth');
		this.ExternalAuthKeyAction = this.createAction<IExternalAuthKeyModel,string>('ExternalAuthKey', 'External Auth Key');
	}
	
	readonly ExternalAuthKeyAction: AppClientAction<IExternalAuthKeyModel,string>;
	
	ExternalAuthKey(model: IExternalAuthKeyModel, errorOptions?: IActionErrorOptions) {
		return this.ExternalAuthKeyAction.execute(model, errorOptions || {});
	}
}