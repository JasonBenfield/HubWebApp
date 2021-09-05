"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LinkListGroup = void 0;
var tslib_1 = require("tslib");
var ListBlockViewModel_1 = require("../Html/ListBlockViewModel");
var BaseListGroup_1 = require("./BaseListGroup");
var LinkListGroupItem_1 = require("./LinkListGroupItem");
var LinkListItemViewModel_1 = require("./LinkListItemViewModel");
var LinkListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(LinkListGroup, _super);
    function LinkListGroup(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new ListBlockViewModel_1.ListBlockViewModel(); }
        return _super.call(this, createItem || (function (itemVM) { return new LinkListGroupItem_1.LinkListGroupItem(itemVM); }), createItemVM || (function () { return new LinkListItemViewModel_1.LinkListItemViewModel(); }), vm) || this;
    }
    return LinkListGroup;
}(BaseListGroup_1.BaseListGroup));
exports.LinkListGroup = LinkListGroup;
//# sourceMappingURL=LinkListGroup.js.map