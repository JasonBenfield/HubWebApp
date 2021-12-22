"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListItem = void 0;
var UserListItem = /** @class */ (function () {
    function UserListItem(user, view) {
        this.user = user;
        view.setUserName(user.UserName);
        view.setFullName(user.Name);
    }
    return UserListItem;
}());
exports.UserListItem = UserListItem;
//# sourceMappingURL=UserListItem.js.map