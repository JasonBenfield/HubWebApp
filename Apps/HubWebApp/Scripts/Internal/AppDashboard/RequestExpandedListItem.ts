import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { RequestExpandedListItemView } from "./RequestExpandedListItemView";

export class RequestExpandedListItem {
    constructor(req: IAppRequestExpandedModel, view: RequestExpandedListItemView) {
        let timeStarted = new FormattedDate(req.TimeStarted).formatDateTime();
        view.setTimeStarted(timeStarted);
        view.setGroupName(req.GroupName);
        view.setActionName(req.ActionName);
        view.setUserName(req.UserName);
    }
}