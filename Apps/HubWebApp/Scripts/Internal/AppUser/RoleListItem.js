"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var RoleListItem = /** @class */ (function () {
    function RoleListItem(role, view) {
        this.role = role;
        new TextBlock_1.TextBlock(role.Name, view.roleName);
    }
    return RoleListItem;
}());
exports.RoleListItem = RoleListItem;
//# sourceMappingURL=RoleListItem.js.map