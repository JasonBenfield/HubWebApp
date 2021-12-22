"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var ModifierListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModifierListItemView, _super);
    function ModifierListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.modKey = row.addColumn()
            .configure(function (c) {
            c.setColumnCss(ColumnCss_1.ColumnCss.xs(4));
            c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass());
        })
            .addContent(new TextSpan_1.TextSpan())
            .configure(function (ts) { return ts.syncTitleWithText(); });
        _this.displayText = row.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpan_1.TextSpan())
            .configure(function (ts) { return ts.syncTitleWithText(); });
        return _this;
    }
    ModifierListItemView.prototype.setModKey = function (modKey) { this.modKey.setText(modKey); };
    ModifierListItemView.prototype.setDisplayText = function (displayText) { this.displayText.setText(displayText); };
    return ModifierListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.ModifierListItemView = ModifierListItemView;
//# sourceMappingURL=ModifierListItemView.js.map