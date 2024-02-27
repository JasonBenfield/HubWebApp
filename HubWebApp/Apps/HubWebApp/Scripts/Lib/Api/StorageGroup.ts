// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class StorageGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Storage');
		this.StoreObjectAction = this.createAction<IStoreObjectRequest,string>('StoreObject', 'Store Object');
		this.GetStoredObjectAction = this.createAction<IGetStoredObjectRequest,string>('GetStoredObject', 'Get Stored Object');
	}
	
	readonly StoreObjectAction: AppApiAction<IStoreObjectRequest,string>;
	readonly GetStoredObjectAction: AppApiAction<IGetStoredObjectRequest,string>;
	
	StoreObject(model: IStoreObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.StoreObjectAction.execute(model, errorOptions || {});
	}
	GetStoredObject(model: IGetStoredObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.GetStoredObjectAction.execute(model, errorOptions || {});
	}
}