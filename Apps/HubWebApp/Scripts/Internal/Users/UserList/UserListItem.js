"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var UserListItem = /** @class */ (function () {
    function UserListItem(user, view) {
        this.user = user;
        new TextBlock_1.TextBlock(user.UserName, view.userName);
        new TextBlock_1.TextBlock(user.Name, view.fullName);
    }
    return UserListItem;
}());
exports.UserListItem = UserListItem;
//# sourceMappingURL=UserListItem.js.map