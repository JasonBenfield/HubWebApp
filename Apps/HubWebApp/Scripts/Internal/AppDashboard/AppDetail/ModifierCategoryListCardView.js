"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ModifierCategoryListItemView_1 = require("./ModifierCategoryListItemView");
var ModifierCategoryListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModifierCategoryListCardView, _super);
    function ModifierCategoryListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.modCategories = _this.addBlockListGroup(function () { return new ModifierCategoryListItemView_1.ModifierCategoryListItemView(); });
        _this.modCategorySelected = _this.modCategories.itemClicked;
        return _this;
    }
    return ModifierCategoryListCardView;
}(CardView_1.CardView));
exports.ModifierCategoryListCardView = ModifierCategoryListCardView;
//# sourceMappingURL=ModifierCategoryListCardView.js.map