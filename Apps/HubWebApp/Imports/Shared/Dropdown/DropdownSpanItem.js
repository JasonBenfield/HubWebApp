"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Span_1 = require("../Html/Span");
var ListItem_1 = require("../Html/ListItem");
var ListItemViewModel_1 = require("../Html/ListItemViewModel");
var DropdownSpanItem = /** @class */ (function (_super) {
    tslib_1.__extends(DropdownSpanItem, _super);
    function DropdownSpanItem(vm) {
        if (vm === void 0) { vm = new ListItemViewModel_1.ListItemViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.span = new Span_1.Span().addToContainer(_this)
            .configure(function (l) {
            l.addCssName('dropdown-item-text');
        });
        return _this;
    }
    return DropdownSpanItem;
}(ListItem_1.ListItem));
exports.DropdownSpanItem = DropdownSpanItem;
//# sourceMappingURL=DropdownSpanItem.js.map