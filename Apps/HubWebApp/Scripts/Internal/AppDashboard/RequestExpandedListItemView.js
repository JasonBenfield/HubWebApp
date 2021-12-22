"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var RequestExpandedListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(RequestExpandedListItemView, _super);
    function RequestExpandedListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.timeStarted = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.groupName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.actionName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.userName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        return _this;
    }
    RequestExpandedListItemView.prototype.setTimeStarted = function (timeStarted) { this.timeStarted.setText(timeStarted); };
    RequestExpandedListItemView.prototype.setGroupName = function (groupName) { this.groupName.setText(groupName); };
    RequestExpandedListItemView.prototype.setActionName = function (actionName) { this.actionName.setText(actionName); };
    RequestExpandedListItemView.prototype.setUserName = function (userName) { this.userName.setText(userName); };
    return RequestExpandedListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.RequestExpandedListItemView = RequestExpandedListItemView;
//# sourceMappingURL=RequestExpandedListItemView.js.map