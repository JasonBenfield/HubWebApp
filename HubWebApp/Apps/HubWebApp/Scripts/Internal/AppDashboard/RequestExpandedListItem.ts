import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ExpandedAppRequest } from "../../Lib/ExpandedAppRequest";
import { RequestExpandedListItemView } from "./RequestExpandedListItemView";

export class RequestExpandedListItem extends BasicComponent {
    constructor(req: ExpandedAppRequest, view: RequestExpandedListItemView) {
        super(view);
        new TextComponent(view.timeStarted).setText(req.timeStarted.format());
        new TextComponent(view.groupName).setText(req.groupName);
        new TextComponent(view.actionName).setText(req.actionName);
        new TextComponent(view.userName).setText(req.userName);
    }
}