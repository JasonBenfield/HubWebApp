"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListCard = void 0;
var tslib_1 = require("tslib");
var ListCard_1 = require("../ListCard");
var ModifierListItemViewModel_1 = require("./ModifierListItemViewModel");
var ModifierListCard = /** @class */ (function (_super) {
    tslib_1.__extends(ModifierListCard, _super);
    function ModifierListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Modifiers were Found') || this;
        _this.hubApi = hubApi;
        vm.title('Modifiers');
        return _this;
    }
    ModifierListCard.prototype.setModCategoryID = function (modCategoryID) {
        this.modCategoryID = modCategoryID;
    };
    ModifierListCard.prototype.createItem = function (sourceItem) {
        var item = new ModifierListItemViewModel_1.ModifierListItemViewModel();
        item.modKey(sourceItem.ModKey);
        item.displayText(sourceItem.DisplayText);
        return item;
    };
    ModifierListCard.prototype.getSourceItems = function () {
        return this.hubApi.ModCategory.GetModifiers(this.modCategoryID);
    };
    return ModifierListCard;
}(ListCard_1.ListCard));
exports.ModifierListCard = ModifierListCard;
//# sourceMappingURL=ModifierListCard.js.map