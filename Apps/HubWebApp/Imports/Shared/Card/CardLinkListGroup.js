"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CardLinkListGroup = void 0;
var tslib_1 = require("tslib");
var ListBlockViewModel_1 = require("../Html/ListBlockViewModel");
var LinkListGroup_1 = require("../ListGroup/LinkListGroup");
var CardLinkListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(CardLinkListGroup, _super);
    function CardLinkListGroup(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new ListBlockViewModel_1.ListBlockViewModel(); }
        var _this = _super.call(this, createItem, createItemVM, vm) || this;
        _this.makeFlush();
        return _this;
    }
    return CardLinkListGroup;
}(LinkListGroup_1.LinkListGroup));
exports.CardLinkListGroup = CardLinkListGroup;
//# sourceMappingURL=CardLinkListGroup.js.map