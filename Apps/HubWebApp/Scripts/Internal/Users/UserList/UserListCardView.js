"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListCardView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var UserListItemView_1 = require("./UserListItemView");
var UserListCardView = /** @class */ (function (_super) {
    tslib_1.__extends(UserListCardView, _super);
    function UserListCardView() {
        var _this = _super.call(this) || this;
        _this.setName(UserListCardView.name);
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.users = _this.addBlockListGroup(function () { return new UserListItemView_1.UserListItemView(); });
        return _this;
    }
    return UserListCardView;
}(CardView_1.CardView));
exports.UserListCardView = UserListCardView;
//# sourceMappingURL=UserListCardView.js.map