"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListPanel = void 0;
var UserListCard_1 = require("./UserListCard");
var UserListPanel = /** @class */ (function () {
    function UserListPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.userListCard = new UserListCard_1.UserListCard(this.vm.userListCard, this.hubApi);
    }
    UserListPanel.prototype.refresh = function () {
        return this.userListCard.refresh();
    };
    return UserListPanel;
}());
exports.UserListPanel = UserListPanel;
//# sourceMappingURL=UserListPanel.js.map