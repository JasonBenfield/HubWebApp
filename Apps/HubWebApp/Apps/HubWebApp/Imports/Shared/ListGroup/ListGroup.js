"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ListGroup = void 0;
var tslib_1 = require("tslib");
var ListItemViewModel_1 = require("../Html/ListItemViewModel");
var UnorderedListViewModel_1 = require("../Html/UnorderedListViewModel");
var BaseListGroup_1 = require("./BaseListGroup");
var ListGroupItem_1 = require("./ListGroupItem");
var ListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(ListGroup, _super);
    function ListGroup(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new UnorderedListViewModel_1.UnorderedListViewModel(); }
        return _super.call(this, createItem || (function (itemVM) { return new ListGroupItem_1.ListGroupItem(itemVM); }), createItemVM || (function () { return new ListItemViewModel_1.ListItemViewModel(); }), vm) || this;
    }
    return ListGroup;
}(BaseListGroup_1.BaseListGroup));
exports.ListGroup = ListGroup;
//# sourceMappingURL=ListGroup.js.map