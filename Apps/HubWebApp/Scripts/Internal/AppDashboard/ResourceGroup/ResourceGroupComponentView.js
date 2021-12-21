"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var ListItem_1 = require("@jasonbenfield/sharedwebapp/Html/ListItem");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ResourceGroupComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroupComponentView, _super);
    function ResourceGroupComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        var listGroup = _this.addUnorderedListGroup();
        var listItem = new ListItem_1.ListItem();
        listGroup.addItem(listItem);
        var row = listItem.addContent(new Row_1.Row());
        _this.groupName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.anonListItem = new ListItem_1.ListItem();
        listGroup.addItem(_this.anonListItem);
        _this.anonListItem.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpan_1.TextSpan('Anonymous is Allowed'));
        return _this;
    }
    ResourceGroupComponentView.prototype.showAnonMessage = function () {
        this.anonListItem.show();
    };
    ResourceGroupComponentView.prototype.hideAnonMessage = function () {
        this.anonListItem.hide();
    };
    ResourceGroupComponentView.prototype.setGroupName = function (groupName) { this.groupName.setText(groupName); };
    return ResourceGroupComponentView;
}(CardView_1.CardView));
exports.ResourceGroupComponentView = ResourceGroupComponentView;
//# sourceMappingURL=ResourceGroupComponentView.js.map