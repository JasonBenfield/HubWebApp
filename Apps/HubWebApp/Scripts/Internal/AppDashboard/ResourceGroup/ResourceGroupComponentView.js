"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var ResourceGroupComponentView = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupComponentView, _super);
    function ResourceGroupComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        var listGroup = _this.addUnorderedListGroup();
        var listItem = new ListGroupItemView_1.ListGroupItemView();
        listGroup.addItem(listItem);
        var row = listItem.addContent(new Row_1.Row());
        _this.groupName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.anonListItem = new ListGroupItemView_1.ListGroupItemView();
        listGroup.addItem(_this.anonListItem);
        _this.anonListItem.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpanView_1.TextSpanView())
            .configure(function (ts) { return ts.setText('Anonymous is Allowed'); });
        return _this;
    }
    ResourceGroupComponentView.prototype.showAnonMessage = function () {
        this.anonListItem.show();
    };
    ResourceGroupComponentView.prototype.hideAnonMessage = function () {
        this.anonListItem.hide();
    };
    return ResourceGroupComponentView;
}(CardView_1.CardView));
exports.ResourceGroupComponentView = ResourceGroupComponentView;
//# sourceMappingURL=ResourceGroupComponentView.js.map