"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var Result_1 = require("XtiShared/Result");
var UserListCard_1 = require("./UserListCard");
var UserListPanel = /** @class */ (function (_super) {
    tslib_1.__extends(UserListPanel, _super);
    function UserListPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.height100();
        _this.setName(UserListPanel.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.userListCard = flexFill.container.addContent(new UserListCard_1.UserListCard(_this.hubApi));
        _this.userListCard.userSelected.register(_this.onUserSelected.bind(_this));
        return _this;
    }
    UserListPanel.prototype.onUserSelected = function (user) {
        this.awaitable.resolve(new Result_1.Result(UserListPanel.ResultKeys.userSelected, user));
    };
    UserListPanel.prototype.refresh = function () {
        return this.userListCard.refresh();
    };
    UserListPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserListPanel.ResultKeys = {
        userSelected: 'user-selected'
    };
    return UserListPanel;
}(Block_1.Block));
exports.UserListPanel = UserListPanel;
//# sourceMappingURL=UserListPanel.js.map