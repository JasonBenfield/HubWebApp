"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var AppListItemView_1 = require("./AppListItemView");
var AppListCardView = /** @class */ (function (_super) {
    tslib_1.__extends(AppListCardView, _super);
    function AppListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.apps = _this.addBlockListGroup(function () { return new AppListItemView_1.AppListItemView(); });
        return _this;
    }
    return AppListCardView;
}(CardView_1.CardView));
exports.AppListCardView = AppListCardView;
//# sourceMappingURL=AppListCardView.js.map