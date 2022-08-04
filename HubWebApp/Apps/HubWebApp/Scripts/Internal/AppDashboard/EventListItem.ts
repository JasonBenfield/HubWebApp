import { FormattedDate } from "@jasonbenfield/sharedwebapp/FormattedDate";
import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { EventListItemView } from "./EventListItemView";

export class EventListItem extends BasicComponent {
    constructor(evt: IAppLogEntryModel, view: EventListItemView) {
        super(view);
        new TextComponent(view.timeOccurred).setText(new FormattedDate(evt.TimeOccurred).formatDateTime());
        new TextComponent(view.severity).setText(evt.Severity.DisplayText);
        const caption = new TextComponent(view.caption);
        caption.setText(evt.Caption);
        caption.syncTitleWithText();
        const message = new TextComponent(view.message)
        message.setText(evt.Message);
        message.syncTitleWithText();
    }
}