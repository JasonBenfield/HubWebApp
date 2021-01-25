"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListPanel = void 0;
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("../../../../Imports/Shared/Result");
var UserListCard_1 = require("./UserListCard");
var UserListPanel = /** @class */ (function () {
    function UserListPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.userListCard = new UserListCard_1.UserListCard(this.vm.userListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.userListCard.userSelected.register(this.onUserSelected.bind(this));
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
}());
exports.UserListPanel = UserListPanel;
//# sourceMappingURL=UserListPanel.js.map