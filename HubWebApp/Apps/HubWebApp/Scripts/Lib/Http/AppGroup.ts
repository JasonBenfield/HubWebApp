// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class AppGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'App');
		this.AddInstallCommandAction = this.createAction<IInstallConfigurationIDRequest,IAppInstallCommandDetailModel>('AddInstallCommand', 'Add Install Command');
		this.ConfigureInstallAction = this.createAction<IConfigureAppInstallRequest,IInstallConfigurationModel>('ConfigureInstall', 'Configure Install');
		this.DeleteInstallConfigurationAction = this.createAction<IInstallConfigurationIDRequest,IEmptyActionResult>('DeleteInstallConfiguration', 'Delete Install Configuration');
		this.GetAppAction = this.createAction<IEmptyRequest,IAppModel>('GetApp', 'Get App');
		this.GetDefaultAppOptionsAction = this.createAction<IEmptyRequest,string>('GetDefaultAppOptions', 'Get Default App Options');
		this.GetDefaultModifierAction = this.createAction<IEmptyRequest,IModifierModel>('GetDefaultModifier', 'Get Default Modifier');
		this.GetDefaultOptionsAction = this.createAction<IEmptyRequest,string>('GetDefaultOptions', 'Get Default Options');
		this.GetInstallConfigurationsAction = this.createAction<IEmptyRequest,IInstallConfigurationModel[]>('GetInstallConfigurations', 'Get Install Configurations');
		this.GetModifierCategoriesAction = this.createAction<IEmptyRequest,IModifierCategoryModel[]>('GetModifierCategories', 'Get Modifier Categories');
		this.GetMostRecentErrorEventsAction = this.createAction<number,IAppLogEntryModel[]>('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
		this.GetMostRecentRequestsAction = this.createAction<number,IAppRequestExpandedModel[]>('GetMostRecentRequests', 'Get Most Recent Requests');
		this.GetResourceGroupsAction = this.createAction<IEmptyRequest,IResourceGroupModel[]>('GetResourceGroups', 'Get Resource Groups');
		this.GetRolesAction = this.createAction<IEmptyRequest,IAppRoleModel[]>('GetRoles', 'Get Roles');
		this.Index = this.createView<IEmptyRequest>('Index');
		this.UpdateVersionsFromPublishedAction = this.createAction<IEmptyRequest,IEmptyActionResult>('UpdateVersionsFromPublished', 'Update Versions From Published');
	}
	
	readonly AddInstallCommandAction: AppClientAction<IInstallConfigurationIDRequest,IAppInstallCommandDetailModel>;
	readonly ConfigureInstallAction: AppClientAction<IConfigureAppInstallRequest,IInstallConfigurationModel>;
	readonly DeleteInstallConfigurationAction: AppClientAction<IInstallConfigurationIDRequest,IEmptyActionResult>;
	readonly GetAppAction: AppClientAction<IEmptyRequest,IAppModel>;
	readonly GetDefaultAppOptionsAction: AppClientAction<IEmptyRequest,string>;
	readonly GetDefaultModifierAction: AppClientAction<IEmptyRequest,IModifierModel>;
	readonly GetDefaultOptionsAction: AppClientAction<IEmptyRequest,string>;
	readonly GetInstallConfigurationsAction: AppClientAction<IEmptyRequest,IInstallConfigurationModel[]>;
	readonly GetModifierCategoriesAction: AppClientAction<IEmptyRequest,IModifierCategoryModel[]>;
	readonly GetMostRecentErrorEventsAction: AppClientAction<number,IAppLogEntryModel[]>;
	readonly GetMostRecentRequestsAction: AppClientAction<number,IAppRequestExpandedModel[]>;
	readonly GetResourceGroupsAction: AppClientAction<IEmptyRequest,IResourceGroupModel[]>;
	readonly GetRolesAction: AppClientAction<IEmptyRequest,IAppRoleModel[]>;
	readonly Index: AppClientView<IEmptyRequest>;
	readonly UpdateVersionsFromPublishedAction: AppClientAction<IEmptyRequest,IEmptyActionResult>;
	
	AddInstallCommand(requestData: IInstallConfigurationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.AddInstallCommandAction.execute(requestData, errorOptions || {});
	}
	ConfigureInstall(requestData: IConfigureAppInstallRequest, errorOptions?: IActionErrorOptions) {
		return this.ConfigureInstallAction.execute(requestData, errorOptions || {});
	}
	DeleteInstallConfiguration(requestData: IInstallConfigurationIDRequest, errorOptions?: IActionErrorOptions) {
		return this.DeleteInstallConfigurationAction.execute(requestData, errorOptions || {});
	}
	GetApp(errorOptions?: IActionErrorOptions) {
		return this.GetAppAction.execute({}, errorOptions || {});
	}
	GetDefaultAppOptions(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultAppOptionsAction.execute({}, errorOptions || {});
	}
	GetDefaultModifier(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultModifierAction.execute({}, errorOptions || {});
	}
	GetDefaultOptions(errorOptions?: IActionErrorOptions) {
		return this.GetDefaultOptionsAction.execute({}, errorOptions || {});
	}
	GetInstallConfigurations(errorOptions?: IActionErrorOptions) {
		return this.GetInstallConfigurationsAction.execute({}, errorOptions || {});
	}
	GetModifierCategories(errorOptions?: IActionErrorOptions) {
		return this.GetModifierCategoriesAction.execute({}, errorOptions || {});
	}
	GetMostRecentErrorEvents(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentErrorEventsAction.execute(requestData, errorOptions || {});
	}
	GetMostRecentRequests(requestData: number, errorOptions?: IActionErrorOptions) {
		return this.GetMostRecentRequestsAction.execute(requestData, errorOptions || {});
	}
	GetResourceGroups(errorOptions?: IActionErrorOptions) {
		return this.GetResourceGroupsAction.execute({}, errorOptions || {});
	}
	GetRoles(errorOptions?: IActionErrorOptions) {
		return this.GetRolesAction.execute({}, errorOptions || {});
	}
	UpdateVersionsFromPublished(errorOptions?: IActionErrorOptions) {
		return this.UpdateVersionsFromPublishedAction.execute({}, errorOptions || {});
	}
}