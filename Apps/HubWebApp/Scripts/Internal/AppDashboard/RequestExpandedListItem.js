"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItem = void 0;
var FormattedDate_1 = require("@jasonbenfield/sharedwebapp/FormattedDate");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var RequestExpandedListItem = /** @class */ (function () {
    function RequestExpandedListItem(req, view) {
        var timeStarted = new FormattedDate_1.FormattedDate(req.TimeStarted).formatDateTime();
        new TextBlock_1.TextBlock(timeStarted, view.timeStarted);
        new TextBlock_1.TextBlock(req.GroupName, view.groupName);
        new TextBlock_1.TextBlock(req.ActionName, view.actionName);
        new TextBlock_1.TextBlock(req.UserName, view.userName);
    }
    return RequestExpandedListItem;
}());
exports.RequestExpandedListItem = RequestExpandedListItem;
//# sourceMappingURL=RequestExpandedListItem.js.map