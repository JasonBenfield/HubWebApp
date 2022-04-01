// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class PublishGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Publish');
		this.NewVersionAction = this.createAction<INewVersionRequest,IXtiVersionModel>('NewVersion', 'New Version');
		this.BeginPublishAction = this.createAction<IPublishVersionRequest,IXtiVersionModel>('BeginPublish', 'Begin Publish');
		this.EndPublishAction = this.createAction<IPublishVersionRequest,IXtiVersionModel>('EndPublish', 'End Publish');
		this.GetVersionsAction = this.createAction<IAppKey,IXtiVersionModel[]>('GetVersions', 'Get Versions');
	}
	
	readonly NewVersionAction: AppApiAction<INewVersionRequest,IXtiVersionModel>;
	readonly BeginPublishAction: AppApiAction<IPublishVersionRequest,IXtiVersionModel>;
	readonly EndPublishAction: AppApiAction<IPublishVersionRequest,IXtiVersionModel>;
	readonly GetVersionsAction: AppApiAction<IAppKey,IXtiVersionModel[]>;
	
	NewVersion(model: INewVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.NewVersionAction.execute(model, errorOptions || {});
	}
	BeginPublish(model: IPublishVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.BeginPublishAction.execute(model, errorOptions || {});
	}
	EndPublish(model: IPublishVersionRequest, errorOptions?: IActionErrorOptions) {
		return this.EndPublishAction.execute(model, errorOptions || {});
	}
	GetVersions(model: IAppKey, errorOptions?: IActionErrorOptions) {
		return this.GetVersionsAction.execute(model, errorOptions || {});
	}
}