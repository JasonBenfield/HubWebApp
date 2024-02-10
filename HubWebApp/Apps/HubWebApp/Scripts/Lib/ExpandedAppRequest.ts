import { DateTimeOffset } from '@jasonbenfield/sharedwebapp/DateTimeOffset';
import { ResourceResultType } from './Http/ResourceResultType';

export class ExpandedAppRequest {
	readonly id: number;
	readonly userName: string;
	readonly groupName: string;
	readonly actionName: string;
	readonly resultType: ResourceResultType;
	readonly timeStarted: DateTimeOffset;
	readonly timeEnded: DateTimeOffset;

	constructor(source: IAppRequestExpandedModel) {
		this.id = source.ID;
		this.userName = source.UserName;
		this.groupName = source.GroupName;
		this.actionName = source.ActionName;
		this.resultType = ResourceResultType.values.value(source.ResultType);
		this.timeStarted = source.TimeStarted;
		this.timeEnded = source.TimeEnded;
    }
}