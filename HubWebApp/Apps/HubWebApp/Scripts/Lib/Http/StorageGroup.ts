// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class StorageGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Storage');
		this.GetStoredObjectAction = this.createAction<IGetStoredObjectRequest,string>('GetStoredObject', 'Get Stored Object');
		this.StoreObjectAction = this.createAction<IStoreObjectRequest,string>('StoreObject', 'Store Object');
	}
	
	readonly GetStoredObjectAction: AppClientAction<IGetStoredObjectRequest,string>;
	readonly StoreObjectAction: AppClientAction<IStoreObjectRequest,string>;
	
	GetStoredObject(model: IGetStoredObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.GetStoredObjectAction.execute(model, errorOptions || {});
	}
	StoreObject(model: IStoreObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.StoreObjectAction.execute(model, errorOptions || {});
	}
}