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
		this.BeginPublishAction = this.createAction<IPublishVersionRequest,IXtiVersionModel>('BeginPublish', 'Begin Publish');
		this.EndPublishAction = this.createAction<IPublishVersionRequest,IXtiVersionModel>('EndPublish', 'End Publish');
		this.GetVersionsAction = this.createAction<IAppKeyRequest,IXtiVersionModel[]>('GetVersions', 'Get Versions');
		this.NewVersionAction = this.createAction<INewVersionRequest,IXtiVersionModel>('NewVersion', 'New Version');
	}
	
	readonly BeginPublishAction: AppClientAction<IPublishVersionRequest,IXtiVersionModel>;
	readonly EndPublishAction: AppClientAction<IPublishVersionRequest,IXtiVersionModel>;
	readonly GetVersionsAction: AppClientAction<IAppKeyRequest,IXtiVersionModel[]>;
	readonly NewVersionAction: AppClientAction<INewVersionRequest,IXtiVersionModel>;
	
	BeginPublish(requestData: IPublishVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginPublishAction.execute(requestData, errorOptions || {});
	}
	EndPublish(requestData: IPublishVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.EndPublishAction.execute(requestData, errorOptions || {});
	}
	GetVersions(requestData: IAppKeyRequest, errorOptions?: IActionErrorOptions) {
		return this.GetVersionsAction.execute(requestData, errorOptions || {});
	}
	NewVersion(requestData: INewVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.NewVersionAction.execute(requestData, errorOptions || {});
	}
}