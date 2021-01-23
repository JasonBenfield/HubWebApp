"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectableListCard = void 0;
var tslib_1 = require("tslib");
var ListCard_1 = require("./ListCard");
var SelectableListCard = /** @class */ (function (_super) {
    tslib_1.__extends(SelectableListCard, _super);
    function SelectableListCard(vm, noItemsFoundMessage) {
        var _this = _super.call(this, vm, noItemsFoundMessage) || this;
        vm.itemSelected.register(_this.onItemSelected.bind(_this));
        return _this;
    }
    return SelectableListCard;
}(ListCard_1.ListCard));
exports.SelectableListCard = SelectableListCard;
//# sourceMappingURL=SelectableListCard.js.map