"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModCategoryPanel = exports.UserModCategoryPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var DropDownFormGroup_1 = require("@jasonbenfield/sharedwebapp/Forms/DropDownFormGroup");
var SelectOption_1 = require("@jasonbenfield/sharedwebapp/Html/SelectOption");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var EditUserModifierListItem_1 = require("./EditUserModifierListItem");
var UserModCategoryPanelResult = /** @class */ (function () {
    function UserModCategoryPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(UserModCategoryPanelResult, "backRequested", {
        get: function () { return new UserModCategoryPanelResult({ backRequested: {} }); },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserModCategoryPanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    return UserModCategoryPanelResult;
}());
exports.UserModCategoryPanelResult = UserModCategoryPanelResult;
var UserModCategoryPanel = /** @class */ (function () {
    function UserModCategoryPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        new CardTitleHeader_1.CardTitleHeader('Edit User Modifiers', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.hasAccessToAll = new DropDownFormGroup_1.DropDownFormGroup('', 'HasAccessToAll', this.view.hasAccessToAll);
        this.hasAccessToAll.setCaption('Has Access to All Modifiers?');
        this.hasAccessToAll.setItems(new SelectOption_1.SelectOption(true, 'Yes'), new SelectOption_1.SelectOption(false, 'No'));
        this.hasAccessToAll.valueChanged.register(this.onHasAccessToAllChanged.bind(this));
        this.userModifiers = new ListGroup_1.ListGroup(this.view.userModifiers);
        this.backCommand.add(this.view.backButton);
    }
    UserModCategoryPanel.prototype.onHasAccessToAllChanged = function (hasAccessToAll) {
        if (hasAccessToAll) {
            this.view.hideUserModifiers();
        }
        else {
            this.view.showUserModifiers();
        }
    };
    UserModCategoryPanel.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserModCategoryPanel.prototype.refresh = function () {
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
                        this.userModifiers.setItems(sourceItems, function (sourceItem, view) {
                            var listItem = new EditUserModifierListItem_1.EditUserModifierListItem(_this.hubApi, view);
                            listItem.setUserID(_this.userID);
                            listItem.withAssignedModifier(sourceItem);
                            return listItem;
                        });
                        return [2 /*return*/];
                }
            });
        });
    };
    UserModCategoryPanel.prototype.compare = function (a, b) {
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
    UserModCategoryPanel.prototype.getUserRoleAccess = function (userID) {
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
    UserModCategoryPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserModCategoryPanel.prototype.back = function () {
        this.awaitable.resolve(UserModCategoryPanelResult.backRequested);
    };
    return UserModCategoryPanel;
}());
exports.UserModCategoryPanel = UserModCategoryPanel;
//# sourceMappingURL=UserModCategoryPanel.js.map