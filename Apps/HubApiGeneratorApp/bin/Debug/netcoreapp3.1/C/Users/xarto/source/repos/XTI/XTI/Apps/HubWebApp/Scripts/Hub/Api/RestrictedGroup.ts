// Generated code

import { AppApiGroup } from "../../Hub/AppApiGroup";
import { AppApiAction } from "../../Hub/AppApiAction";
import { AppApiView } from "../../Hub/AppApiView";
import { AppApiEvents } from "../../Hub/AppApiEvents";
import { AppResourceUrl } from "../../Hub/AppResourceUrl";

export class RestrictedGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Restricted');
		this.Index = this.createView<IEmptyRequest>('Index');
	}

	readonly Index: AppApiView<IEmptyRequest>;

}