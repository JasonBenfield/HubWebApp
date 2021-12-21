import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { EventListItemView } from "./EventListItemView";

export class EventListItem {
    constructor(evt: IAppEventModel, view: EventListItemView) {
        view.setTimeOccurred(new FormattedDate(evt.TimeOccurred).formatDateTime());
        view.setSeverity(evt.Severity.DisplayText);
        view.setCaption(evt.Caption);
        view.setMessage(evt.Message);
    }
}