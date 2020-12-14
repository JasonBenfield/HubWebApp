// Generated code

import { AppApiGroup } from "../../Shared/AppApiGroup";
import { AppApiAction } from "../../Shared/AppApiAction";
import { AppApiView } from "../../Shared/AppApiView";
import { AppApiEvents } from "../../Shared/AppApiEvents";
import { AppResourceUrl } from "../../Shared/AppResourceUrl";

export class UserAdminGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'UserAdmin');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.AddUserAction = this.createAction<IAddUserModel,number>('AddUser', 'AddUser');
	}

	readonly Index: AppApiView<IEmptyRequest>;
	private readonly AddUserAction: AppApiAction<IAddUserModel,number>;

	AddUser(model: IAddUserModel, errorOptions?: IActionErrorOptions) {
		return this.AddUserAction.execute(model, errorOptions || {});
	}
}