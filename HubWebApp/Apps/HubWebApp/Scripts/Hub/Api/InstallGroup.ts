// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class InstallGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Install');
		this.RegisterAppAction = this.createAction<IRegisterAppRequest,IAppWithModKeyModel>('RegisterApp', 'Register App');
		this.AddOrUpdateAppsAction = this.createAction<IAddOrUpdateAppsRequest,IAppModel[]>('AddOrUpdateApps', 'Add Or Update Apps');
		this.AddOrUpdateVersionsAction = this.createAction<IAddOrUpdateVersionsRequest,IEmptyActionResult>('AddOrUpdateVersions', 'Add Or Update Versions');
		this.GetVersionAction = this.createAction<IGetVersionRequest,IXtiVersionModel>('GetVersion', 'Get Version');
		this.GetVersionsAction = this.createAction<IGetVersionsRequest,IXtiVersionModel[]>('GetVersions', 'Get Versions');
		this.AddSystemUserAction = this.createAction<IAddSystemUserRequest,IAppUserModel>('AddSystemUser', 'Add System User');
		this.AddInstallationUserAction = this.createAction<IAddInstallationUserRequest,IAppUserModel>('AddInstallationUser', 'Add Installation User');
		this.NewInstallationAction = this.createAction<INewInstallationRequest,INewInstallationResult>('NewInstallation', 'New Installation');
		this.BeginInstallationAction = this.createAction<IInstallationRequest,IEmptyActionResult>('BeginInstallation', 'Begin Installation');
		this.InstalledAction = this.createAction<IInstallationRequest,IEmptyActionResult>('Installed', 'Installed');
		this.SetUserAccessAction = this.createAction<ISetUserAccessRequest,IEmptyActionResult>('SetUserAccess', 'Set User Access');
	}
	
	readonly RegisterAppAction: AppApiAction<IRegisterAppRequest,IAppWithModKeyModel>;
	readonly AddOrUpdateAppsAction: AppApiAction<IAddOrUpdateAppsRequest,IAppModel[]>;
	readonly AddOrUpdateVersionsAction: AppApiAction<IAddOrUpdateVersionsRequest,IEmptyActionResult>;
	readonly GetVersionAction: AppApiAction<IGetVersionRequest,IXtiVersionModel>;
	readonly GetVersionsAction: AppApiAction<IGetVersionsRequest,IXtiVersionModel[]>;
	readonly AddSystemUserAction: AppApiAction<IAddSystemUserRequest,IAppUserModel>;
	readonly AddInstallationUserAction: AppApiAction<IAddInstallationUserRequest,IAppUserModel>;
	readonly NewInstallationAction: AppApiAction<INewInstallationRequest,INewInstallationResult>;
	readonly BeginInstallationAction: AppApiAction<IInstallationRequest,IEmptyActionResult>;
	readonly InstalledAction: AppApiAction<IInstallationRequest,IEmptyActionResult>;
	readonly SetUserAccessAction: AppApiAction<ISetUserAccessRequest,IEmptyActionResult>;
	
	RegisterApp(model: IRegisterAppRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterAppAction.execute(model, errorOptions || {});
	}
	AddOrUpdateApps(model: IAddOrUpdateAppsRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateAppsAction.execute(model, errorOptions || {});
	}
	AddOrUpdateVersions(model: IAddOrUpdateVersionsRequest, errorOptions?: IActionErrorOptions) {
		return this.AddOrUpdateVersionsAction.execute(model, errorOptions || {});
	}
	GetVersion(model: IGetVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.GetVersionAction.execute(model, errorOptions || {});
	}
	GetVersions(model: IGetVersionsRequest, errorOptions?: IActionErrorOptions) {
		return this.GetVersionsAction.execute(model, errorOptions || {});
	}
	AddSystemUser(model: IAddSystemUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddSystemUserAction.execute(model, errorOptions || {});
	}
	AddInstallationUser(model: IAddInstallationUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddInstallationUserAction.execute(model, errorOptions || {});
	}
	NewInstallation(model: INewInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.NewInstallationAction.execute(model, errorOptions || {});
	}
	BeginInstallation(model: IInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginInstallationAction.execute(model, errorOptions || {});
	}
	Installed(model: IInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.InstalledAction.execute(model, errorOptions || {});
	}
	SetUserAccess(model: ISetUserAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.SetUserAccessAction.execute(model, errorOptions || {});
	}
}