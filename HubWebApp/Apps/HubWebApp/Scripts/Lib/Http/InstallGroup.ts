// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class InstallGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Install');
		this.RegisterAppAction = this.createAction<IRegisterAppRequest,IAppModel>('RegisterApp', 'Register App');
		this.AddOrUpdateAppsAction = this.createAction<IAddOrUpdateAppsRequest,IAppModel[]>('AddOrUpdateApps', 'Add Or Update Apps');
		this.AddOrUpdateVersionsAction = this.createAction<IAddOrUpdateVersionsRequest,IEmptyActionResult>('AddOrUpdateVersions', 'Add Or Update Versions');
		this.GetVersionAction = this.createAction<IGetVersionRequest,IXtiVersionModel>('GetVersion', 'Get Version');
		this.GetVersionsAction = this.createAction<IGetVersionsRequest,IXtiVersionModel[]>('GetVersions', 'Get Versions');
		this.AddSystemUserAction = this.createAction<IAddSystemUserRequest,IAppUserModel>('AddSystemUser', 'Add System User');
		this.AddAdminUserAction = this.createAction<IAddAdminUserRequest,IAppUserModel>('AddAdminUser', 'Add Admin User');
		this.AddInstallationUserAction = this.createAction<IAddInstallationUserRequest,IAppUserModel>('AddInstallationUser', 'Add Installation User');
		this.BeginInstallationAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('BeginInstallation', 'Begin Installation');
		this.ConfigureInstallTemplateAction = this.createAction<IConfigureInstallTemplateRequest,IInstallConfigurationTemplateModel>('ConfigureInstallTemplate', 'Configure Install Template');
		this.ConfigureInstallAction = this.createAction<IConfigureInstallRequest,IInstallConfigurationModel>('ConfigureInstall', 'Configure Install');
		this.DeleteInstallConfigurationAction = this.createAction<IDeleteInstallConfigurationRequest,IEmptyActionResult>('DeleteInstallConfiguration', 'Delete Install Configuration');
		this.GetInstallConfigurationsAction = this.createAction<IGetInstallConfigurationsRequest,IInstallConfigurationModel[]>('GetInstallConfigurations', 'Get Install Configurations');
		this.InstalledAction = this.createAction<IGetInstallationRequest,IEmptyActionResult>('Installed', 'Installed');
		this.NewInstallationAction = this.createAction<INewInstallationRequest,INewInstallationResult>('NewInstallation', 'New Installation');
		this.SetUserAccessAction = this.createAction<ISetUserAccessRequest,IEmptyActionResult>('SetUserAccess', 'Set User Access');
	}
	
	readonly RegisterAppAction: AppClientAction<IRegisterAppRequest,IAppModel>;
	readonly AddOrUpdateAppsAction: AppClientAction<IAddOrUpdateAppsRequest,IAppModel[]>;
	readonly AddOrUpdateVersionsAction: AppClientAction<IAddOrUpdateVersionsRequest,IEmptyActionResult>;
	readonly GetVersionAction: AppClientAction<IGetVersionRequest,IXtiVersionModel>;
	readonly GetVersionsAction: AppClientAction<IGetVersionsRequest,IXtiVersionModel[]>;
	readonly AddSystemUserAction: AppClientAction<IAddSystemUserRequest,IAppUserModel>;
	readonly AddAdminUserAction: AppClientAction<IAddAdminUserRequest,IAppUserModel>;
	readonly AddInstallationUserAction: AppClientAction<IAddInstallationUserRequest,IAppUserModel>;
	readonly BeginInstallationAction: AppClientAction<IGetInstallationRequest,IEmptyActionResult>;
	readonly ConfigureInstallTemplateAction: AppClientAction<IConfigureInstallTemplateRequest,IInstallConfigurationTemplateModel>;
	readonly ConfigureInstallAction: AppClientAction<IConfigureInstallRequest,IInstallConfigurationModel>;
	readonly DeleteInstallConfigurationAction: AppClientAction<IDeleteInstallConfigurationRequest,IEmptyActionResult>;
	readonly GetInstallConfigurationsAction: AppClientAction<IGetInstallConfigurationsRequest,IInstallConfigurationModel[]>;
	readonly InstalledAction: AppClientAction<IGetInstallationRequest,IEmptyActionResult>;
	readonly NewInstallationAction: AppClientAction<INewInstallationRequest,INewInstallationResult>;
	readonly SetUserAccessAction: AppClientAction<ISetUserAccessRequest,IEmptyActionResult>;
	
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
	AddAdminUser(model: IAddAdminUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddAdminUserAction.execute(model, errorOptions || {});
	}
	AddInstallationUser(model: IAddInstallationUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddInstallationUserAction.execute(model, errorOptions || {});
	}
	BeginInstallation(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginInstallationAction.execute(model, errorOptions || {});
	}
	ConfigureInstallTemplate(model: IConfigureInstallTemplateRequest, errorOptions?: IActionErrorOptions) {
		return this.ConfigureInstallTemplateAction.execute(model, errorOptions || {});
	}
	ConfigureInstall(model: IConfigureInstallRequest, errorOptions?: IActionErrorOptions) {
		return this.ConfigureInstallAction.execute(model, errorOptions || {});
	}
	DeleteInstallConfiguration(model: IDeleteInstallConfigurationRequest, errorOptions?: IActionErrorOptions) {
		return this.DeleteInstallConfigurationAction.execute(model, errorOptions || {});
	}
	GetInstallConfigurations(model: IGetInstallConfigurationsRequest, errorOptions?: IActionErrorOptions) {
		return this.GetInstallConfigurationsAction.execute(model, errorOptions || {});
	}
	Installed(model: IGetInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.InstalledAction.execute(model, errorOptions || {});
	}
	NewInstallation(model: INewInstallationRequest, errorOptions?: IActionErrorOptions) {
		return this.NewInstallationAction.execute(model, errorOptions || {});
	}
	SetUserAccess(model: ISetUserAccessRequest, errorOptions?: IActionErrorOptions) {
		return this.SetUserAccessAction.execute(model, errorOptions || {});
	}
}