"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ModifierListItemView_1 = require("./ModifierListItemView");
var ModifierListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModifierListCardView, _super);
    function ModifierListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        _this.modifiers = _this.addBlockListGroup(function () { return new ModifierListItemView_1.ModifierListItemView(); });
        return _this;
    }
    return ModifierListCardView;
}(CardView_1.CardView));
exports.ModifierListCardView = ModifierListCardView;
//# sourceMappingURL=ModifierListCardView.js.map