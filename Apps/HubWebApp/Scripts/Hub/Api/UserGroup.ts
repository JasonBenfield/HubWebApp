// Generated code

import { AppApiGroup } from "../../Hub/AppApiGroup";
import { AppApiAction } from "../../Hub/AppApiAction";
import { AppApiView } from "../../Hub/AppApiView";
import { AppApiEvents } from "../../Hub/AppApiEvents";
import { AppResourceUrl } from "../../Hub/AppResourceUrl";

export class UserGroup extends AppApiGroup implements IUserGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'User');
		this.Index = this.createView<IUserStartRequest>('Index');
	}

	readonly Index: AppApiView<IUserStartRequest>;

}