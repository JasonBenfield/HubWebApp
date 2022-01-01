// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class InstallGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Install');
		this.RegisterAppAction = this.createAction<IRegisterAppRequest,IEmptyActionResult>('RegisterApp', 'Register App');
		this.GetVersionAction = this.createAction<IGetVersionRequest,IAppVersionModel>('GetVersion', 'Get Version');
		this.AddSystemUserAction = this.createAction<IAddSystemUserRequest,IAppUserModel>('AddSystemUser', 'Add System User');
		this.NewInstallationAction = this.createAction<INewInstallationRequest,INewInstallationResult>('NewInstallation', 'New Installation');
		this.BeginCurrentInstallationAction = this.createAction<IBeginInstallationRequest,number>('BeginCurrentInstallation', 'Begin Current Installation');
		this.BeginVersionInstallationAction = this.createAction<IBeginInstallationRequest,number>('BeginVersionInstallation', 'Begin Version Installation');
		this.InstalledAction = this.createAction<IInstalledRequest,IEmptyActionResult>('Installed', 'Installed');
	}
	
	readonly RegisterAppAction: AppApiAction<IRegisterAppRequest,IEmptyActionResult>;
	readonly GetVersionAction: AppApiAction<IGetVersionRequest,IAppVersionModel>;
	readonly AddSystemUserAction: AppApiAction<IAddSystemUserRequest,IAppUserModel>;
	readonly NewInstallationAction: AppApiAction<INewInstallationRequest,INewInstallationResult>;
	readonly BeginCurrentInstallationAction: AppApiAction<IBeginInstallationRequest,number>;
	readonly BeginVersionInstallationAction: AppApiAction<IBeginInstallationRequest,number>;
	readonly InstalledAction: AppApiAction<IInstalledRequest,IEmptyActionResult>;
	
	RegisterApp(model: IRegisterAppRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterAppAction.execute(model, errorOptions || {});
	}
	GetVersion(model: IGetVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.GetVersionAction.execute(model, errorOptions || {});
	}
	AddSystemUser(model: IAddSystemUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddSystemUserAction.execute(model, errorOptions || {});
	}
	NewInstallation(model: INewInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.NewInstallationAction.execute(model, errorOptions || {});
	}
	BeginCurrentInstallation(model: IBeginInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginCurrentInstallationAction.execute(model, errorOptions || {});
	}
	BeginVersionInstallation(model: IBeginInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginVersionInstallationAction.execute(model, errorOptions || {});
	}
	Installed(model: IInstalledRequest, errorOptions?: IActionErrorOptions) {
		return this.InstalledAction.execute(model, errorOptions || {});
	}
}