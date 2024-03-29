// Generated code

import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class SystemGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
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
	
	readonly GetAppContextAction: AppClientAction<IGetAppContextRequest,IAppContextModel>;
	readonly GetUserContextAction: AppClientAction<IGetUserContextRequest,IUserContextModel>;
	readonly AddOrUpdateModifierByTargetKeyAction: AppClientAction<ISystemAddOrUpdateModifierByTargetKeyRequest,IModifierModel>;
	readonly AddOrUpdateModifierByModKeyAction: AppClientAction<ISystemAddOrUpdateModifierByModKeyRequest,IModifierModel>;
	readonly GetUserOrAnonAction: AppClientAction<string,IAppUserModel>;
	readonly GetUserAuthenticatorsAction: AppClientAction<number,IUserAuthenticatorModel[]>;
	readonly GetUsersWithAnyRoleAction: AppClientAction<ISystemGetUsersWithAnyRoleRequest,IAppUserModel[]>;
	readonly StoreObjectAction: AppClientAction<IStoreObjectRequest,string>;
	readonly GetStoredObjectAction: AppClientAction<IGetStoredObjectRequest,string>;
	readonly SetUserAccessAction: AppClientAction<ISystemSetUserAccessRequest,IEmptyActionResult>;
	
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