// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class SystemGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'System');
		this.AddOrUpdateModifierByModKeyAction = this.createAction<ISystemAddOrUpdateModifierByModKeyRequest,IModifierModel>('AddOrUpdateModifierByModKey', 'Add Or Update Modifier By Mod Key');
		this.AddOrUpdateModifierByTargetKeyAction = this.createAction<ISystemAddOrUpdateModifierByTargetKeyRequest,IModifierModel>('AddOrUpdateModifierByTargetKey', 'Add Or Update Modifier By Target Key');
		this.GetAppContextAction = this.createAction<IGetAppContextRequest,IAppContextModel>('GetAppContext', 'Get App Context');
		this.GetModifierAction = this.createAction<IGetModifierRequest,IModifierModel>('GetModifier', 'Get Modifier');
		this.GetStoredObjectAction = this.createAction<IGetStoredObjectRequest,string>('GetStoredObject', 'Get Stored Object');
		this.GetUserAuthenticatorsAction = this.createAction<IAppUserIDRequest,IUserAuthenticatorModel[]>('GetUserAuthenticators', 'Get User Authenticators');
		this.GetUserByUserNameAction = this.createAction<IAppUserNameRequest,IAppUserModel>('GetUserByUserName', 'Get User By User Name');
		this.GetUserOrAnonAction = this.createAction<IAppUserNameRequest,IAppUserModel>('GetUserOrAnon', 'Get User Or Anon');
		this.GetUserRolesAction = this.createAction<IGetUserRolesRequest,IAppRoleModel[]>('GetUserRoles', 'Get User Roles');
		this.GetUsersWithAnyRoleAction = this.createAction<ISystemGetUsersWithAnyRoleRequest,IAppUserModel[]>('GetUsersWithAnyRole', 'Get Users With Any Role');
		this.SetUserAccessAction = this.createAction<ISystemSetUserAccessRequest,IEmptyActionResult>('SetUserAccess', 'Set User Access');
		this.StoreObjectAction = this.createAction<IStoreObjectRequest,string>('StoreObject', 'Store Object');
	}
	
	readonly AddOrUpdateModifierByModKeyAction: AppClientAction<ISystemAddOrUpdateModifierByModKeyRequest,IModifierModel>;
	readonly AddOrUpdateModifierByTargetKeyAction: AppClientAction<ISystemAddOrUpdateModifierByTargetKeyRequest,IModifierModel>;
	readonly GetAppContextAction: AppClientAction<IGetAppContextRequest,IAppContextModel>;
	readonly GetModifierAction: AppClientAction<IGetModifierRequest,IModifierModel>;
	readonly GetStoredObjectAction: AppClientAction<IGetStoredObjectRequest,string>;
	readonly GetUserAuthenticatorsAction: AppClientAction<IAppUserIDRequest,IUserAuthenticatorModel[]>;
	readonly GetUserByUserNameAction: AppClientAction<IAppUserNameRequest,IAppUserModel>;
	readonly GetUserOrAnonAction: AppClientAction<IAppUserNameRequest,IAppUserModel>;
	readonly GetUserRolesAction: AppClientAction<IGetUserRolesRequest,IAppRoleModel[]>;
	readonly GetUsersWithAnyRoleAction: AppClientAction<ISystemGetUsersWithAnyRoleRequest,IAppUserModel[]>;
	readonly SetUserAccessAction: AppClientAction<ISystemSetUserAccessRequest,IEmptyActionResult>;
	readonly StoreObjectAction: AppClientAction<IStoreObjectRequest,string>;
	
	AddOrUpdateModifierByModKey(model: ISystemAddOrUpdateModifierByModKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByModKeyAction.execute(model, errorOptions || {});
	}
	AddOrUpdateModifierByTargetKey(model: ISystemAddOrUpdateModifierByTargetKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByTargetKeyAction.execute(model, errorOptions || {});
	}
	GetAppContext(model: IGetAppContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppContextAction.execute(model, errorOptions || {});
	}
	GetModifier(model: IGetModifierRequest, errorOptions?: IActionErrorOptions) {
		return this.GetModifierAction.execute(model, errorOptions || {});
	}
	GetStoredObject(model: IGetStoredObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.GetStoredObjectAction.execute(model, errorOptions || {});
	}
	GetUserAuthenticators(model: IAppUserIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserAuthenticatorsAction.execute(model, errorOptions || {});
	}
	GetUserByUserName(model: IAppUserNameRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserByUserNameAction.execute(model, errorOptions || {});
	}
	GetUserOrAnon(model: IAppUserNameRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserOrAnonAction.execute(model, errorOptions || {});
	}
	GetUserRoles(model: IGetUserRolesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRolesAction.execute(model, errorOptions || {});
	}
	GetUsersWithAnyRole(model: ISystemGetUsersWithAnyRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUsersWithAnyRoleAction.execute(model, errorOptions || {});
	}
	SetUserAccess(model: ISystemSetUserAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.SetUserAccessAction.execute(model, errorOptions || {});
	}
	StoreObject(model: IStoreObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.StoreObjectAction.execute(model, errorOptions || {});
	}
}