"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ListItem_1 = require("../Html/ListItem");
var ListItemViewModel_1 = require("../Html/ListItemViewModel");
var UnorderedList_1 = require("../Html/UnorderedList");
var UnorderedListViewModel_1 = require("../Html/UnorderedListViewModel");
var DropdownMenu = /** @class */ (function (_super) {
    tslib_1.__extends(DropdownMenu, _super);
    function DropdownMenu(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new UnorderedListViewModel_1.UnorderedListViewModel(); }
        var _this = _super.call(this, createItem || (function (itemVM) { return new ListItem_1.ListItem(itemVM); }), createItemVM || (function () { return new ListItemViewModel_1.ListItemViewModel(); }), vm) || this;
        _this.addCssName('dropdown-menu dropdown-menu-right');
        return _this;
    }
    return DropdownMenu;
}(UnorderedList_1.UnorderedList));
exports.DropdownMenu = DropdownMenu;
//# sourceMappingURL=DropdownMenu.js.map