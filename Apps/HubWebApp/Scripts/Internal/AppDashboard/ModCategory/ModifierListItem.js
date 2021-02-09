"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListItem = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var TextCss_1 = require("XtiShared/TextCss");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var ModifierListItem = /** @class */ (function (_super) {
    tslib_1.__extends(ModifierListItem, _super);
    function ModifierListItem(modifier, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addColumn()
            .configure(function (c) {
            c.setColumnCss(ColumnCss_1.ColumnCss.xs(4));
            c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass());
        })
            .addContent(new TextSpan_1.TextSpan(modifier.ModKey))
            .configure(function (ts) { return ts.setTitleFromText(); });
        _this.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpan_1.TextSpan(modifier.DisplayText))
            .configure(function (ts) { return ts.setTitleFromText(); });
        return _this;
    }
    return ModifierListItem;
}(Row_1.Row));
exports.ModifierListItem = ModifierListItem;
//# sourceMappingURL=ModifierListItem.js.map