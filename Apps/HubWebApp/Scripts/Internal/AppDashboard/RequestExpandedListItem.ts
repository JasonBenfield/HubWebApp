import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { RequestExpandedListItemView } from "./RequestExpandedListItemView";

export class RequestExpandedListItem {
    constructor(req: IAppRequestExpandedModel, view: RequestExpandedListItemView) {
        let timeStarted = new FormattedDate(req.TimeStarted).formatDateTime();
        new TextBlock(timeStarted, view.timeStarted);
        new TextBlock(req.GroupName, view.groupName);
        new TextBlock(req.ActionName, view.actionName);
        new TextBlock(req.UserName, view.userName);
    }
}