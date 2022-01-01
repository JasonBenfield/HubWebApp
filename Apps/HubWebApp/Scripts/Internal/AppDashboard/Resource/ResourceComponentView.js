"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var ListItem_1 = require("@jasonbenfield/sharedwebapp/Html/ListItem");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ResourceComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceComponentView, _super);
    function ResourceComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        var listGroup = _this.addUnorderedListGroup();
        var listItem = new ListItem_1.ListItem();
        listGroup.addItem(listItem);
        var row = listItem.addContent(new Row_1.Row());
        _this.resourceName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpanView_1.TextSpanView());
        _this.resultType = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.anonListItem = new ListItem_1.ListItem();
        listGroup.addItem(_this.anonListItem);
        _this.anonListItem.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpanView_1.TextSpanView())
            .configure(function (ts) { return ts.setText('Anonymous is Allowed'); });
        _this.anonListItem.hide();
        return _this;
    }
    ResourceComponentView.prototype.showAnon = function () {
        this.anonListItem.show();
    };
    ResourceComponentView.prototype.hideAnon = function () {
        this.anonListItem.hide();
    };
    return ResourceComponentView;
}(CardView_1.CardView));
exports.ResourceComponentView = ResourceComponentView;
//# sourceMappingURL=ResourceComponentView.js.map