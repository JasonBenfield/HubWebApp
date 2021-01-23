"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItem = void 0;
var FormattedDate_1 = require("XtiShared/FormattedDate");
var EventListItem = /** @class */ (function () {
    function EventListItem(vm, evt) {
        vm.timeOccurred(new FormattedDate_1.FormattedDate(evt.TimeOccurred).formatDateTime());
        vm.severity(evt.Severity.DisplayText);
        vm.caption(evt.Caption);
        vm.message(evt.Message);
    }
    return EventListItem;
}());
exports.EventListItem = EventListItem;
//# sourceMappingURL=EventListItem.js.map