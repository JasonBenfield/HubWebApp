"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CardButtonListGroup = void 0;
var tslib_1 = require("tslib");
var ListBlockViewModel_1 = require("../Html/ListBlockViewModel");
var ButtonListGroup_1 = require("../ListGroup/ButtonListGroup");
var CardButtonListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(CardButtonListGroup, _super);
    function CardButtonListGroup(createItem, createItemVM, vm) {
        if (createItem === void 0) { createItem = null; }
        if (createItemVM === void 0) { createItemVM = null; }
        if (vm === void 0) { vm = new ListBlockViewModel_1.ListBlockViewModel(); }
        var _this = _super.call(this, createItem, createItemVM, vm) || this;
        _this.makeFlush();
        return _this;
    }
    return CardButtonListGroup;
}(ButtonListGroup_1.ButtonListGroup));
exports.CardButtonListGroup = CardButtonListGroup;
//# sourceMappingURL=CardButtonListGroup.js.map