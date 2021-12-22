"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItem = void 0;
var FormattedDate_1 = require("@jasonbenfield/sharedwebapp/FormattedDate");
var EventListItem = /** @class */ (function () {
    function EventListItem(evt, view) {
        view.setTimeOccurred(new FormattedDate_1.FormattedDate(evt.TimeOccurred).formatDateTime());
        view.setSeverity(evt.Severity.DisplayText);
        view.setCaption(evt.Caption);
        view.setMessage(evt.Message);
    }
    return EventListItem;
}());
exports.EventListItem = EventListItem;
//# sourceMappingURL=EventListItem.js.map