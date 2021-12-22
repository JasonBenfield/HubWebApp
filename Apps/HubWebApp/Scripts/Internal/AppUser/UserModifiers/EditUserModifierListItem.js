"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserModifierListItem = void 0;
var tslib_1 = require("tslib");
var EditUserModifierListItem = /** @class */ (function () {
    function EditUserModifierListItem(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.view.clicked.register(this.onClick.bind(this));
    }
    EditUserModifierListItem.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    EditUserModifierListItem.prototype.onClick = function () {
        return this.toggleAssignment();
    };
    EditUserModifierListItem.prototype.toggleAssignment = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var request, _a;
            return (0, tslib_1.__generator)(this, function (_b) {
                switch (_b.label) {
                    case 0:
                        this.view.startAssignment();
                        _b.label = 1;
                    case 1:
                        _b.trys.push([1, , 6, 7]);
                        if (!this.roleID) return [3 /*break*/, 3];
                        request = {
                            UserID: this.userID,
                            RoleID: this.roleID
                        };
                        return [4 /*yield*/, this.hubApi.AppUserMaintenance.UnassignRole(request)];
                    case 2:
                        _b.sent();
                        this.view.unassign();
                        return [3 /*break*/, 5];
                    case 3:
                        _a = this;
                        return [4 /*yield*/, this.hubApi.AppUserMaintenance.AssignRole({
                                UserID: this.userID,
                                RoleID: this.roleID
                            })];
                    case 4:
                        _a.roleID = _b.sent();
                        this.view.assign();
                        _b.label = 5;
                    case 5: return [3 /*break*/, 7];
                    case 6:
                        this.view.endAssignment();
                        return [7 /*endfinally*/];
                    case 7: return [2 /*return*/];
                }
            });
        });
    };
    EditUserModifierListItem.prototype.withAssignedModifier = function (userRole) {
        this.view.setModKey(userRole.Name);
        this.roleID = userRole.ID;
        this.view.assign();
    };
    EditUserModifierListItem.prototype.withUnassignedModifier = function (modifier) {
        this.view.setModKey(modifier.ModKey);
        this.view.setModDisplayText(modifier.DisplayText);
        this.roleID = null;
        this.roleID = modifier.ID;
        this.view.unassign();
    };
    return EditUserModifierListItem;
}());
exports.EditUserModifierListItem = EditUserModifierListItem;
//# sourceMappingURL=EditUserModifierListItem.js.map