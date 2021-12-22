"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModCategoryListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var UserModCategoryListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserModCategoryListCardView, _super);
    function UserModCategoryListCardView() {
        var _this = _super.call(this) || this;
        _this.modCategoryComponents = [];
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        return _this;
    }
    return UserModCategoryListCardView;
}(CardView_1.CardView));
exports.UserModCategoryListCardView = UserModCategoryListCardView;
//# sourceMappingURL=UserModCategoryListCardView.js.map