"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserRoleListItem = void 0;
var tslib_1 = require("tslib");
var EditUserRoleListItem = /** @class */ (function () {
    function EditUserRoleListItem(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.view.clicked.register(this.onClick.bind(this));
    }
    EditUserRoleListItem.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    EditUserRoleListItem.prototype.onClick = function () {
        return this.toggleAssignment();
    };
    EditUserRoleListItem.prototype.toggleAssignment = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.view.startAssignment();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, , 3, 4]);
                        return [4 /*yield*/, this.hubApi.AppUserMaintenance.AssignRole({
                                UserID: this.userID,
                                RoleID: this.roleID
                            })];
                    case 2:
                        _a.sent();
                        this.view.assign();
                        return [3 /*break*/, 4];
                    case 3:
                        this.view.endAssignment();
                        return [7 /*endfinally*/];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    EditUserRoleListItem.prototype.withAssignedRole = function (userRole) {
        this.view.setRoleName(userRole.Name);
        this.roleID = userRole.ID;
        this.view.assign();
    };
    EditUserRoleListItem.prototype.withUnassignedRole = function (role) {
        this.view.setRoleName(role.Name);
        this.roleID = role.ID;
        this.view.unassign();
    };
    return EditUserRoleListItem;
}());
exports.EditUserRoleListItem = EditUserRoleListItem;
//# sourceMappingURL=EditUserRoleListItem.js.map