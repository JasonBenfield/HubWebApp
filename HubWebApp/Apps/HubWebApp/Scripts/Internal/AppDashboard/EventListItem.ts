import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { TextBlock } from "@jasonbenfield/sharedwebapp/Html/TextBlock";
import { EventListItemView } from "./EventListItemView";

export class EventListItem {
    constructor(evt: IAppLogEntryModel, view: EventListItemView) {
        new TextBlock(new FormattedDate(evt.TimeOccurred).formatDateTime(), view.timeOccurred);
        new TextBlock(evt.Severity.DisplayText, view.severity);
        let caption = new TextBlock(evt.Caption, view.caption);
        caption.syncTitleWithText();
        let message = new TextBlock(evt.Message, view.message);
        message.syncTitleWithText();
    }
}