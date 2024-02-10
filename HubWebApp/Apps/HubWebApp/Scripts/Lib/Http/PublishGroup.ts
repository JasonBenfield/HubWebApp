// Generated code

import * as xti from "@jasonbenfield/sharedwebapp/Common";
import { AppClientGroup } from "@jasonbenfield/sharedwebapp/Http/AppClientGroup";
import { AppClientAction } from "@jasonbenfield/sharedwebapp/Http/AppClientAction";
import { AppClientView } from "@jasonbenfield/sharedwebapp/Http/AppClientView";
import { AppClientEvents } from "@jasonbenfield/sharedwebapp/Http/AppClientEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Http/AppResourceUrl";

export class PublishGroup extends AppClientGroup {
	constructor(events: AppClientEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Publish');
		this.NewVersionAction = this.createAction<INewVersionRequest,IXtiVersionModel>('NewVersion', 'New Version');
		this.BeginPublishAction = this.createAction<IPublishVersionRequest,IXtiVersionModel>('BeginPublish', 'Begin Publish');
		this.EndPublishAction = this.createAction<IPublishVersionRequest,IXtiVersionModel>('EndPublish', 'End Publish');
		this.GetVersionsAction = this.createAction<IAppKeyRequest,IXtiVersionModel[]>('GetVersions', 'Get Versions');
	}
	
	readonly NewVersionAction: AppClientAction<INewVersionRequest,IXtiVersionModel>;
	readonly BeginPublishAction: AppClientAction<IPublishVersionRequest,IXtiVersionModel>;
	readonly EndPublishAction: AppClientAction<IPublishVersionRequest,IXtiVersionModel>;
	readonly GetVersionsAction: AppClientAction<IAppKeyRequest,IXtiVersionModel[]>;
	
	NewVersion(model: INewVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.NewVersionAction.execute(model, errorOptions || {});
	}
	BeginPublish(model: IPublishVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginPublishAction.execute(model, errorOptions || {});
	}
	EndPublish(model: IPublishVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.EndPublishAction.execute(model, errorOptions || {});
	}
	GetVersions(model: IAppKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.GetVersionsAction.execute(model, errorOptions || {});
	}
}