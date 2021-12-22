"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var UserListCardView_1 = require("./UserListCardView");
var UserListPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserListPanelView, _super);
    function UserListPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        _this.setName(UserListPanelView.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.userListCard = flexFill.container.addContent(new UserListCardView_1.UserListCardView());
        return _this;
    }
    return UserListPanelView;
}(Block_1.Block));
exports.UserListPanelView = UserListPanelView;
//# sourceMappingURL=UserListPanelView.js.map