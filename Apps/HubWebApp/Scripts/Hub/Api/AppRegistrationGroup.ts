// Generated code

import { AppApiGroup } from "XtiShared/AppApiGroup";
import { AppApiAction } from "XtiShared/AppApiAction";
import { AppApiView } from "XtiShared/AppApiView";
import { AppApiEvents } from "XtiShared/AppApiEvents";
import { AppResourceUrl } from "XtiShared/AppResourceUrl";

export class AppRegistrationGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'AppRegistration');
		this.RegisterAppAction = this.createAction<IRegisterAppRequest,IEmptyActionResult>('RegisterApp', 'Register App');
		this.NewVersionAction = this.createAction<INewVersionRequest,IAppVersionModel>('NewVersion', 'New Version');
		this.BeginPublishAction = this.createAction<IGetVersionRequest,IAppVersionModel>('BeginPublish', 'Begin Publish');
		this.EndPublishAction = this.createAction<IGetVersionRequest,IAppVersionModel>('EndPublish', 'End Publish');
		this.GetVersionsAction = this.createAction<IAppKey,IAppVersionModel[]>('GetVersions', 'Get Versions');
		this.GetVersionAction = this.createAction<IGetVersionRequest,IAppVersionModel>('GetVersion', 'Get Version');
		this.AddSystemUserAction = this.createAction<IAddSystemUserRequest,IAppUserModel>('AddSystemUser', 'Add System User');
	}
	
	readonly RegisterAppAction: AppApiAction<IRegisterAppRequest,IEmptyActionResult>;
	readonly NewVersionAction: AppApiAction<INewVersionRequest,IAppVersionModel>;
	readonly BeginPublishAction: AppApiAction<IGetVersionRequest,IAppVersionModel>;
	readonly EndPublishAction: AppApiAction<IGetVersionRequest,IAppVersionModel>;
	readonly GetVersionsAction: AppApiAction<IAppKey,IAppVersionModel[]>;
	readonly GetVersionAction: AppApiAction<IGetVersionRequest,IAppVersionModel>;
	readonly AddSystemUserAction: AppApiAction<IAddSystemUserRequest,IAppUserModel>;
	
	RegisterApp(model: IRegisterAppRequest, errorOptions?: IActionErrorOptions) {
		return this.RegisterAppAction.execute(model, errorOptions || {});
	}
	NewVersion(model: INewVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.NewVersionAction.execute(model, errorOptions || {});
	}
	BeginPublish(model: IGetVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginPublishAction.execute(model, errorOptions || {});
	}
	EndPublish(model: IGetVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.EndPublishAction.execute(model, errorOptions || {});
	}
	GetVersions(model: IAppKey, errorOptions?: IActionErrorOptions) {
		return this.GetVersionsAction.execute(model, errorOptions || {});
	}
	GetVersion(model: IGetVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.GetVersionAction.execute(model, errorOptions || {});
	}
	AddSystemUser(model: IAddSystemUserRequest, errorOptions?: IActionErrorOptions) {
		return this.AddSystemUserAction.execute(model, errorOptions || {});
	}
}