"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListItem = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ResourceGroupListItem = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupListItem, _super);
    function ResourceGroupListItem(rg, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(rg.Name));
        return _this;
    }
    return ResourceGroupListItem;
}(Row_1.Row));
exports.ResourceGroupListItem = ResourceGroupListItem;
//# sourceMappingURL=ResourceGroupListItem.js.map