"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItem = void 0;
var FormattedDate_1 = require("@jasonbenfield/sharedwebapp/FormattedDate");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var EventListItem = /** @class */ (function () {
    function EventListItem(evt, view) {
        new TextBlock_1.TextBlock(new FormattedDate_1.FormattedDate(evt.TimeOccurred).formatDateTime(), view.timeOccurred);
        new TextBlock_1.TextBlock(evt.Severity.DisplayText, view.severity);
        var caption = new TextBlock_1.TextBlock(evt.Caption, view.caption);
        caption.syncTitleWithText();
        var message = new TextBlock_1.TextBlock(evt.Message, view.message);
        message.syncTitleWithText();
    }
    return EventListItem;
}());
exports.EventListItem = EventListItem;
//# sourceMappingURL=EventListItem.js.map