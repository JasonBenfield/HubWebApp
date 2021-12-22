"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRolePanel = exports.UserRolePanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var EditUserRoleListItem_1 = require("./EditUserRoleListItem");
var UserRolePanelResult = /** @class */ (function () {
    function UserRolePanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(UserRolePanelResult, "backRequested", {
        get: function () {
            return new UserRolePanelResult({ backRequested: {} });
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserRolePanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    return UserRolePanelResult;
}());
exports.UserRolePanelResult = UserRolePanelResult;
var UserRolePanel = /** @class */ (function () {
    function UserRolePanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        new CardTitleHeader_1.CardTitleHeader('Edit User Roles', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.userRoles = new ListGroup_1.ListGroup(this.view.userRoles);
        this.backCommand.add(this.view.backButton);
    }
    UserRolePanel.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserRolePanel.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var access, sourceItems, _i, _a, userRole, _b, _c, role;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_d) {
                switch (_d.label) {
                    case 0: return [4 /*yield*/, this.getUserRoleAccess(this.userID)];
                    case 1:
                        access = _d.sent();
                        sourceItems = [];
                        for (_i = 0, _a = access.AssignedRoles; _i < _a.length; _i++) {
                            userRole = _a[_i];
                            sourceItems.push(userRole);
                        }
                        for (_b = 0, _c = access.UnassignedRoles; _b < _c.length; _b++) {
                            role = _c[_b];
                            sourceItems.push(role);
                        }
                        sourceItems.sort(this.compare.bind(this));
                        this.userRoles.setItems(sourceItems, function (role, view) {
                            var listItem = new EditUserRoleListItem_1.EditUserRoleListItem(_this.hubApi, view);
                            listItem.setUserID(_this.userID);
                            listItem.withAssignedRole(role);
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    UserRolePanel.prototype.compare = function (a, b) {
        var roleName;
        roleName = a.Name;
        var otherRoleName;
        otherRoleName = b.Name;
        var result;
        if (roleName < otherRoleName) {
            result = -1;
        }
        else if (roleName === otherRoleName) {
            result = 0;
        }
        else {
            result = 1;
        }
        return result;
    };
    UserRolePanel.prototype.getUserRoleAccess = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var access;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            var request;
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0:
                                        request = {
                                            UserID: userID,
                                            ModifierID: 0
                                        };
                                        return [4 /*yield*/, this.hubApi.AppUser.GetUserRoleAccess(request)];
                                    case 1:
                                        access = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, access];
                }
            });
        });
    };
    UserRolePanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserRolePanel.prototype.back = function () {
        this.awaitable.resolve(UserRolePanelResult.backRequested);
    };
    UserRolePanel.prototype.activate = function () { this.view.show(); };
    UserRolePanel.prototype.deactivate = function () { this.view.hide(); };
    return UserRolePanel;
}());
exports.UserRolePanel = UserRolePanel;
//# sourceMappingURL=UserRolePanel.js.map