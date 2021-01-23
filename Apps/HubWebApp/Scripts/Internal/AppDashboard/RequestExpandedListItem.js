"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItem = void 0;
var FormattedDate_1 = require("XtiShared/FormattedDate");
var RequestExpandedListItem = /** @class */ (function () {
    function RequestExpandedListItem(vm, req) {
        vm.groupName(req.GroupName);
        vm.actionName(req.ActionName);
        var timeStarted = new FormattedDate_1.FormattedDate(req.TimeStarted).formatDateTime();
        vm.timeStarted(timeStarted);
        vm.userName(req.UserName);
    }
    return RequestExpandedListItem;
}());
exports.RequestExpandedListItem = RequestExpandedListItem;
//# sourceMappingURL=RequestExpandedListItem.js.map