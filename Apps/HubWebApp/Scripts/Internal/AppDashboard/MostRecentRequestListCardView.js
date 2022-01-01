"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentRequestListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var RequestExpandedListItemView_1 = require("./RequestExpandedListItemView");
var MostRecentRequestListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(MostRecentRequestListCardView, _super);
    function MostRecentRequestListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.requests = _this.addBlockListGroup(function () { return new RequestExpandedListItemView_1.RequestExpandedListItemView(); });
        return _this;
    }
    return MostRecentRequestListCardView;
}(CardView_1.CardView));
exports.MostRecentRequestListCardView = MostRecentRequestListCardView;
//# sourceMappingURL=MostRecentRequestListCardView.js.map