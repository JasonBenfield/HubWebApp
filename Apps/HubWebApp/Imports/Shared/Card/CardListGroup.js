"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CardListGroup = void 0;
var tslib_1 = require("tslib");
var ListItemViewModel_1 = require("../Html/ListItemViewModel");
var UnorderedListViewModel_1 = require("../Html/UnorderedListViewModel");
var ListGroup_1 = require("../ListGroup/ListGroup");
var ListGroupItem_1 = require("../ListGroup/ListGroupItem");
var CardListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(CardListGroup, _super);
    function CardListGroup(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new UnorderedListViewModel_1.UnorderedListViewModel(); }
        var _this = _super.call(this, createItem || (function (itemVM) { return new ListGroupItem_1.ListGroupItem(itemVM); }), createItemVM || (function () { return new ListItemViewModel_1.ListItemViewModel(); }), vm) || this;
        _this.makeFlush();
        return _this;
    }
    return CardListGroup;
}(ListGroup_1.ListGroup));
exports.CardListGroup = CardListGroup;
//# sourceMappingURL=CardListGroup.js.map