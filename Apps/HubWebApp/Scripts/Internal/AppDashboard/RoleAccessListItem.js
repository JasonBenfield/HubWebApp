"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleAccessListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var RoleAccessListItem = /** @class */ (function () {
    function RoleAccessListItem(accessItem, view) {
        if (accessItem.isAllowed) {
            view.allowAccess();
        }
        else {
            view.denyAccess();
        }
        new TextBlock_1.TextBlock(accessItem.role.Name, view.roleName);
    }
    return RoleAccessListItem;
}());
exports.RoleAccessListItem = RoleAccessListItem;
//# sourceMappingURL=RoleAccessListItem.js.map