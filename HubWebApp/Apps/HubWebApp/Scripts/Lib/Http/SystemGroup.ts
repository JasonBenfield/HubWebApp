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
	
	AddOrUpdateModifierByModKey(requestData: ISystemAddOrUpdateModifierByModKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByModKeyAction.execute(requestData, errorOptions || {});
	}
	AddOrUpdateModifierByTargetKey(requestData: ISystemAddOrUpdateModifierByTargetKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateModifierByTargetKeyAction.execute(requestData, errorOptions || {});
	}
	GetAppContext(requestData: IGetAppContextRequest, errorOptions?: IActionErrorOptions) {
		return this.GetAppContextAction.execute(requestData, errorOptions || {});
	}
	GetModifier(requestData: IGetModifierRequest, errorOptions?: IActionErrorOptions) {
		return this.GetModifierAction.execute(requestData, errorOptions || {});
	}
	GetStoredObject(requestData: IGetStoredObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.GetStoredObjectAction.execute(requestData, errorOptions || {});
	}
	GetUserAuthenticators(requestData: IAppUserIDRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserAuthenticatorsAction.execute(requestData, errorOptions || {});
	}
	GetUserByUserName(requestData: IAppUserNameRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserByUserNameAction.execute(requestData, errorOptions || {});
	}
	GetUserOrAnon(requestData: IAppUserNameRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserOrAnonAction.execute(requestData, errorOptions || {});
	}
	GetUserRoles(requestData: IGetUserRolesRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUserRolesAction.execute(requestData, errorOptions || {});
	}
	GetUsersWithAnyRole(requestData: ISystemGetUsersWithAnyRoleRequest, errorOptions?: IActionErrorOptions) {
		return this.GetUsersWithAnyRoleAction.execute(requestData, errorOptions || {});
	}
	SetUserAccess(requestData: ISystemSetUserAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.SetUserAccessAction.execute(requestData, errorOptions || {});
	}
	StoreObject(requestData: IStoreObjectRequest, errorOptions?: IActionErrorOptions) {
		return this.StoreObjectAction.execute(requestData, errorOptions || {});
	}
}