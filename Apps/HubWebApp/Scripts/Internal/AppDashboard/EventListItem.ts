import { FormattedDate } from "XtiShared/FormattedDate";
import { EventListItemViewModel } from "./EventListItemViewModel";

export class EventListItem {
    constructor(vm: EventListItemViewModel, evt: IAppEventModel) {
        vm.timeOccurred(new FormattedDate(evt.TimeOccurred).formatDateTime());
        vm.severity(evt.Severity.DisplayText);
        vm.caption(evt.Caption);
        vm.message(evt.Message);
    }
}