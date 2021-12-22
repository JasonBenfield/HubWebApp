// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/AppResourceUrl";

export class PublishGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Publish');
		this.NewVersionAction = this.createAction<INewVersionRequest,IAppVersionModel>('NewVersion', 'New Version');
		this.BeginPublishAction = this.createAction<IPublishVersionRequest,IAppVersionModel>('BeginPublish', 'Begin Publish');
		this.EndPublishAction = this.createAction<IPublishVersionRequest,IAppVersionModel>('EndPublish', 'End Publish');
		this.GetVersionsAction = this.createAction<IAppKey,IAppVersionModel[]>('GetVersions', 'Get Versions');
	}
	
	readonly NewVersionAction: AppApiAction<INewVersionRequest,IAppVersionModel>;
	readonly BeginPublishAction: AppApiAction<IPublishVersionRequest,IAppVersionModel>;
	readonly EndPublishAction: AppApiAction<IPublishVersionRequest,IAppVersionModel>;
	readonly GetVersionsAction: AppApiAction<IAppKey,IAppVersionModel[]>;
	
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