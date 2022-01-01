"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ResourceGroupListItemView_1 = require("../ResourceGroupListItemView");
var ResourceGroupListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroupListCardView, _super);
    function ResourceGroupListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.requests = _this.addBlockListGroup(function () { return new ResourceGroupListItemView_1.ResourceGroupListItemView(); });
        return _this;
    }
    return ResourceGroupListCardView;
}(CardView_1.CardView));
exports.ResourceGroupListCardView = ResourceGroupListCardView;
//# sourceMappingURL=ResourceGroupListCardView.js.map