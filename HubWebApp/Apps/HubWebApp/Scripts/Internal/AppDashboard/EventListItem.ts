import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { AppLogEntry } from "../../Lib/AppLogEntry";
import { EventListItemView } from "./EventListItemView";

export class EventListItem extends BasicComponent {
    constructor(evt: AppLogEntry, view: EventListItemView) {
        super(view);
        new TextComponent(view.timeOccurred).setText(evt.timeOccurred.format());
        new TextComponent(view.severity).setText(evt.severity.DisplayText);
        const caption = new TextComponent(view.caption);
        caption.setText(evt.caption);
        caption.syncTitleWithText();
        const message = new TextComponent(view.message)
        message.setText(evt.message);
        message.syncTitleWithText();
    }
}