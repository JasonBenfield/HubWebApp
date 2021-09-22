"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleAccessListItem = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var TextCss_1 = require("XtiShared/TextCss");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var ContextualClass_1 = require("XtiShared/ContextualClass");
var FaIcon_1 = require("XtiShared/FaIcon");
var RoleAccessListItem = /** @class */ (function (_super) {
    tslib_1.__extends(RoleAccessListItem, _super);
    function RoleAccessListItem(accessItem, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new FaIcon_1.FaIcon(accessItem.isAllowed ? 'thumbs-up' : 'thumbs-down'))
            .configure(function (icon) {
            icon.regularStyle();
            icon.makeFixedWidth();
            icon.addCssFrom(new TextCss_1.TextCss().context(accessItem.isAllowed
                ? ContextualClass_1.ContextualClass.success
                : ContextualClass_1.ContextualClass.danger).cssClass());
        });
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(accessItem.role.Name));
        return _this;
    }
    return RoleAccessListItem;
}(Row_1.Row));
exports.RoleAccessListItem = RoleAccessListItem;
//# sourceMappingURL=RoleAccessListItem.js.map