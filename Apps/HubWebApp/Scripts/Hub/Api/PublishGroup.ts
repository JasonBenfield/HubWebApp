// Generated code

import { AppApiGroup } from "@jasonbenfield/sharedwebapp/Api/AppApiGroup";
import { AppApiAction } from "@jasonbenfield/sharedwebapp/Api/AppApiAction";
import { AppApiView } from "@jasonbenfield/sharedwebapp/Api/AppApiView";
import { AppApiEvents } from "@jasonbenfield/sharedwebapp/Api/AppApiEvents";
import { AppResourceUrl } from "@jasonbenfield/sharedwebapp/Api/AppResourceUrl";

export class PublishGroup extends AppApiGroup {
	constructor(events: AppApiEvents, resourceUrl: AppResourceUrl) {
		super(events, resourceUrl, 'Publish');
		this.NextVersionKeyAction = this.createAction<IEmptyRequest,IAppVersionKey>('NextVersionKey', 'Next Version Key');
		this.NewVersionAction = this.createAction<INewVersionRequest,IAppVersionModel>('NewVersion', 'New Version');
		this.BeginPublishAction = this.createAction<IPublishVersionRequest,IAppVersionModel>('BeginPublish', 'Begin Publish');
		this.EndPublishAction = this.createAction<IPublishVersionRequest,IAppVersionModel>('EndPublish', 'End Publish');
		this.GetVersionsAction = this.createAction<IAppKey,IAppVersionModel[]>('GetVersions', 'Get Versions');
	}
	
	readonly NextVersionKeyAction: AppApiAction<IEmptyRequest,IAppVersionKey>;
	readonly NewVersionAction: AppApiAction<INewVersionRequest,IAppVersionModel>;
	readonly BeginPublishAction: AppApiAction<IPublishVersionRequest,IAppVersionModel>;
	readonly EndPublishAction: AppApiAction<IPublishVersionRequest,IAppVersionModel>;
	readonly GetVersionsAction: AppApiAction<IAppKey,IAppVersionModel[]>;
	
	NextVersionKey(errorOptions?: IActionErrorOptions) {
		return this.NextVersionKeyAction.execute({}, errorOptions || {});
	}
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