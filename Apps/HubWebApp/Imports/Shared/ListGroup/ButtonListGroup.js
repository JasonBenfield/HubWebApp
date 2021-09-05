"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ButtonListGroup = void 0;
var tslib_1 = require("tslib");
var ListBlockViewModel_1 = require("../Html/ListBlockViewModel");
var BaseListGroup_1 = require("./BaseListGroup");
var ButtonListGroupItem_1 = require("./ButtonListGroupItem");
var ButtonListItemViewModel_1 = require("./ButtonListItemViewModel");
var ButtonListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(ButtonListGroup, _super);
    function ButtonListGroup(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new ListBlockViewModel_1.ListBlockViewModel(); }
        return _super.call(this, createItem || (function (itemVM) { return new ButtonListGroupItem_1.ButtonListGroupItem(itemVM); }), createItemVM || (function () { return new ButtonListItemViewModel_1.ButtonListItemViewModel(); }), vm) || this;
    }
    return ButtonListGroup;
}(BaseListGroup_1.BaseListGroup));
exports.ButtonListGroup = ButtonListGroup;
//# sourceMappingURL=ButtonListGroup.js.map