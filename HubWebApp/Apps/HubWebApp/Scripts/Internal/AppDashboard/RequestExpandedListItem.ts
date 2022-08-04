import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { RequestExpandedListItemView } from "./RequestExpandedListItemView";

export class RequestExpandedListItem extends BasicComponent {
    constructor(req: IAppRequestExpandedModel, view: RequestExpandedListItemView) {
        super(view);
        const timeStarted = new FormattedDate(req.TimeStarted).formatDateTime();
        new TextComponent(view.timeStarted).setText(timeStarted);
        new TextComponent(view.groupName).setText(req.GroupName);
        new TextComponent(view.actionName).setText(req.ActionName);
        new TextComponent(view.userName).setText(req.UserName);
    }
}