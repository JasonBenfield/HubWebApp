"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleAccessListItem = void 0;
var RoleAccessListItem = /** @class */ (function () {
    function RoleAccessListItem(accessItem, view) {
        if (accessItem.isAllowed) {
            view.allowAccess();
        }
        else {
            view.denyAccess();
        }
        view.setRoleName(accessItem.role.Name);
    }
    return RoleAccessListItem;
}());
exports.RoleAccessListItem = RoleAccessListItem;
//# sourceMappingURL=RoleAccessListItem.js.map