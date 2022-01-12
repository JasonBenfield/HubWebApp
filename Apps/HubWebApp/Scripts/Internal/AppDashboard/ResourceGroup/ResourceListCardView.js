"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ResourceListItemView_1 = require("./ResourceListItemView");
var ResourceListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceListCardView, _super);
    function ResourceListCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.resources = _this.addBlockListGroup(function () { return new ResourceListItemView_1.ResourceListItemView(); });
        return _this;
    }
    return ResourceListCardView;
}(CardView_1.CardView));
exports.ResourceListCardView = ResourceListCardView;
//# sourceMappingURL=ResourceListCardView.js.map