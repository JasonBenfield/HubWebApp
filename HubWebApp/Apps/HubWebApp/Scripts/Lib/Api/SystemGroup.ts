// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class SystemGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'System');
		this.GetAppContextAction = this.createAction<IGetAppContextRequest,IAppContextModel>('GetAppContext', 'Get App Context');
		this.GetUserContextAction = this.createAction<IGetUserContextRequest,IUserContextModel>('GetUserContext', 'Get User Context');
		this.AddOrUpdateModifierByTargetKeyAction = this.createAction<ISystemAddOrUpdateModifierByTargetKeyRequest,IModifierModel>('AddOrUpdateModifierByTargetKey', 'Add Or Update Modifier By Target Key');
		this.AddOrUpdateModifierByModKeyAction = this.createAction<ISystemAddOrUpdateModifierByModKeyRequest,IModifierModel>('AddOrUpdateModifierByModKey', 'Add Or Update Modifier By Mod Key');
		this.GetUserOrAnonAction = this.createAction<string,IAppUserModel>('GetUserOrAnon', 'Get User Or Anon');
		this.GetUserAuthenticatorsAction = this.createAction<number,IUserAuthenticatorModel[]>('GetUserAuthenticators', 'Get User Authenticators');
		this.GetUsersWithAnyRoleAction = this.createAction<ISystemGetUsersWithAnyRoleRequest,IAppUserModel[]>('GetUsersWithAnyRole', 'Get Users With Any Role');
		this.StoreObjectAction = this.createAction<IStoreObjectRequest,string>('StoreObject', 'Store Object');
		this.GetStoredObjectAction = this.createAction<IGetStoredObjectRequest,string>('GetStoredObject', 'Get Stored Object');
		this.SetUserAccessAction = this.createAction<ISystemSetUserAccessRequest,IEmptyActionResult>('SetUserAccess', 'Set User Access');
	}
	
	readonly GetAppContextAction: AppApiAction<IGetAppContextRequest,IAppContextModel>;
	readonly GetUserContextAction: AppApiAction<IGetUserContextRequest,IUserContextModel>;
	readonly AddOrUpdateModifierByTargetKeyAction: AppApiAction<ISystemAddOrUpdateModifierByTargetKeyRequest,IModifierModel>;
	readonly AddOrUpdateModifierByModKeyAction: AppApiAction<ISystemAddOrUpdateModifierByModKeyRequest,IModifierModel>;
	readonly GetUserOrAnonAction: AppApiAction<string,IAppUserModel>;
	readonly GetUserAuthenticatorsAction: AppApiAction<number,IUserAuthenticatorModel[]>;
	readonly GetUsersWithAnyRoleAction: AppApiAction<ISystemGetUsersWithAnyRoleRequest,IAppUserModel[]>;
	readonly StoreObjectAction: AppApiAction<IStoreObjectRequest,string>;
	readonly GetStoredObjectAction: AppApiAction<IGetStoredObjectRequest,string>;
	readonly SetUserAccessAction: AppApiAction<ISystemSetUserAccessRequest,IEmptyActionResult>;
	
	GetAppContext(model: IGetAppContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppContextAction.execute(model, errorOptions || {});
	}
	GetUserContext(model: IGetUserContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserContextAction.execute(model, errorOptions || {});
	}
	AddOrUpdateModifierByTargetKey(model: ISystemAddOrUpdateModifierByTargetKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByTargetKeyAction.execute(model, errorOptions || {});
	}
	AddOrUpdateModifierByModKey(model: ISystemAddOrUpdateModifierByModKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByModKeyAction.execute(model, errorOptions || {});
	}
	GetUserOrAnon(model: string, errorOptions?: IActionErrorOptions) {
		return this.GetUserOrAnonAction.execute(model, errorOptions || {});
	}
	GetUserAuthenticators(model: number, errorOptions?: IActionErrorOptions) {
		return this.GetUserAuthenticatorsAction.execute(model, errorOptions || {});
	}
	GetUsersWithAnyRole(model: ISystemGetUsersWithAnyRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUsersWithAnyRoleAction.execute(model, errorOptions || {});
	}
	StoreObject(model: IStoreObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.StoreObjectAction.execute(model, errorOptions || {});
	}
	GetStoredObject(model: IGetStoredObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.GetStoredObjectAction.execute(model, errorOptions || {});
	}
	SetUserAccess(model: ISystemSetUserAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.SetUserAccessAction.execute(model, errorOptions || {});
	}
}