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

    constructor(source?: IAppRequestExpandedModel) {
        this.id = source ? source.ID : 0;
        this.userName = source ? source.UserName : "";
        this.groupName = source ? source.GroupName : "";
        this.actionName = source ? source.ActionName : "";
        this.resultType = source ? ResourceResultType.values.value(source.ResultType) : ResourceResultType.values.None;
        this.timeStarted = source ? source.TimeStarted : DateTimeOffset.max();
        this.timeEnded = source ? source.TimeEnded : DateTimeOffset.max();
    }
}