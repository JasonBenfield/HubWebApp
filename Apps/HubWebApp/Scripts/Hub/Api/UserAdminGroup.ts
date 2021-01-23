// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class UserAdminGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserAdmin');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.AddUserAction = this.createAction<IAddUserModel,number>('AddUser', 'Add User');
	}
	
	readonly Index: AppApiView<IEmptyRequest>;
	readonly AddUserAction: AppApiAction<IAddUserModel,number>;
	
	AddUser(model: IAddUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddUserAction.execute(model, errorOptions || {});
	}
}