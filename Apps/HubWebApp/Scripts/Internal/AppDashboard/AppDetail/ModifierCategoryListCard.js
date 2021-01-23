"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var SelectableListCard_1 = require("../SelectableListCard");
var ModifierCategoryListItemViewModel_1 = require("./ModifierCategoryListItemViewModel");
var ModifierCategoryListCard = /** @class */ (function (_super) {
    tslib_1.__extends(ModifierCategoryListCard, _super);
    function ModifierCategoryListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Modifier Categories were Found') || this;
        _this.hubApi = hubApi;
        _this._modCategorySelected = new Events_1.DefaultEvent(_this);
        _this.modCategorySelected = _this._modCategorySelected.handler();
        vm.title('Modifier Categories');
        return _this;
    }
    ModifierCategoryListCard.prototype.onItemSelected = function (item) {
        this._modCategorySelected.invoke(item.source);
    };
    ModifierCategoryListCard.prototype.createItem = function (r) {
        var item = new ModifierCategoryListItemViewModel_1.ModifierCategoryListItemViewModel(r);
        item.name(r.Name);
        return item;
    };
    ModifierCategoryListCard.prototype.getSourceItems = function () {
        return this.hubApi.App.GetModifierCategories();
    };
    return ModifierCategoryListCard;
}(SelectableListCard_1.SelectableListCard));
exports.ModifierCategoryListCard = ModifierCategoryListCard;
//# sourceMappingURL=ModifierCategoryListCard.js.map