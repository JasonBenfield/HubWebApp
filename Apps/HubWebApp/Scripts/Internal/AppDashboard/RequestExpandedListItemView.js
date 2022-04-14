"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var RequestExpandedListItemView = /** @class */ (function (_super) {
    tslib_1.__extends(RequestExpandedListItemView, _super);
    function RequestExpandedListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.timeStarted = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.groupName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.actionName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.userName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return RequestExpandedListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.RequestExpandedListItemView = RequestExpandedListItemView;
//# sourceMappingURL=RequestExpandedListItemView.js.map