"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceAccessCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var RoleAccessListItemView_1 = require("./RoleAccessListItemView");
var ResourceAccessCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceAccessCardView, _super);
    function ResourceAccessCardView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        _this.accessItems = _this.addUnorderedListGroup(function () { return new RoleAccessListItemView_1.RoleAccessListItemView(); });
        return _this;
    }
    return ResourceAccessCardView;
}(CardView_1.CardView));
exports.ResourceAccessCardView = ResourceAccessCardView;
//# sourceMappingURL=ResourceAccessCardView.js.map