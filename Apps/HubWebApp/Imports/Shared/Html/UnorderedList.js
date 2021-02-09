"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BaseList_1 = require("./BaseList");
var ListItem_1 = require("./ListItem");
var ListItemViewModel_1 = require("./ListItemViewModel");
var UnorderedListViewModel_1 = require("./UnorderedListViewModel");
var UnorderedList = /** @class */ (function (_super) {
    tslib_1.__extends(UnorderedList, _super);
    function UnorderedList(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new UnorderedListViewModel_1.UnorderedListViewModel(); }
        return _super.call(this, createItem || (function (itemVM) { return new ListItem_1.ListItem(itemVM); }), createItemVM || (function () { return new ListItemViewModel_1.ListItemViewModel(); }), vm) || this;
    }
    return UnorderedList;
}(BaseList_1.BaseList));
exports.UnorderedList = UnorderedList;
//# sourceMappingURL=UnorderedList.js.map