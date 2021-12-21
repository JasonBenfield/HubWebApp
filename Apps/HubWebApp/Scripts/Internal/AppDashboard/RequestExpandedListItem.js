"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItem = void 0;
var FormattedDate_1 = require("@jasonbenfield/sharedwebapp/FormattedDate");
var RequestExpandedListItem = /** @class */ (function () {
    function RequestExpandedListItem(req, view) {
        var timeStarted = new FormattedDate_1.FormattedDate(req.TimeStarted).formatDateTime();
        view.setTimeStarted(timeStarted);
        view.setGroupName(req.GroupName);
        view.setActionName(req.ActionName);
        view.setUserName(req.UserName);
    }
    return RequestExpandedListItem;
}());
exports.RequestExpandedListItem = RequestExpandedListItem;
//# sourceMappingURL=RequestExpandedListItem.js.map