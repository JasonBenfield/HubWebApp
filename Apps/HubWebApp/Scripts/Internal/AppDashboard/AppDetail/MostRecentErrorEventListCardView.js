"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentErrorEventListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var EventListItemView_1 = require("../EventListItemView");
var MostRecentErrorEventListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(MostRecentErrorEventListCardView, _super);
    function MostRecentErrorEventListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        _this.errorEvents = _this.addUnorderedListGroup(function () { return new EventListItemView_1.EventListItemView(); });
        return _this;
    }
    return MostRecentErrorEventListCardView;
}(CardView_1.CardView));
exports.MostRecentErrorEventListCardView = MostRecentErrorEventListCardView;
//# sourceMappingURL=MostRecentErrorEventListCardView.js.map