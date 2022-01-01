"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRolesPanel = exports.UserRolesPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var AsyncCommand_1 = require("@jasonbenfield/sharedwebapp/Command/AsyncCommand");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var UserRoleListItem_1 = require("./UserRoleListItem");
var UserRolesPanelResult = /** @class */ (function () {
    function UserRolesPanelResult(results) {
        this.results = results;
    }
    UserRolesPanelResult.addRequested = function () { return new UserRolesPanelResult({ addRequested: {} }); };
    UserRolesPanelResult.modifierRequested = function () {
        return new UserRolesPanelResult({ modifierRequested: {} });
    };
    Object.defineProperty(UserRolesPanelResult.prototype, "addRequested", {
        get: function () { return this.results.addRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserRolesPanelResult.prototype, "modifierRequested", {
        get: function () { return this.results.modifierRequested; },
        enumerable: false,
        configurable: true
    });
    return UserRolesPanelResult;
}());
exports.UserRolesPanelResult = UserRolesPanelResult;
var UserRolesPanel = /** @class */ (function () {
    function UserRolesPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.userRoleListItems = [];
        this.deleteEvents = new Events_1.EventCollection();
        this.appName = new TextBlock_1.TextBlock('', view.appName);
        this.appType = new TextBlock_1.TextBlock('', view.appType);
        this.userName = new TextBlock_1.TextBlock('', view.userName);
        this.personName = new TextBlock_1.TextBlock('', view.personName);
        this.categoryName = new TextBlock_1.TextBlock('', view.categoryName);
        this.modifierDisplayText = new TextBlock_1.TextBlock('', view.modifierDisplayText);
        this.alert = new CardAlert_1.CardAlert(view.alert).alert;
        this.userRoles = new ListGroup_1.ListGroup(view.userRoles);
        this.awaitable = new Awaitable_1.Awaitable();
        this.addCommand = new Command_1.Command(this.requestAdd.bind(this));
        this.addCommand.add(view.addButton);
        new Command_1.Command(this.requestModifier.bind(this)).add(view.selectModifierButton);
        this.allowAccessCommand = new AsyncCommand_1.AsyncCommand(this.allowAccess.bind(this));
        this.allowAccessCommand.add(view.allowAccessButton);
        this.denyAccessCommand = new AsyncCommand_1.AsyncCommand(this.denyAccess.bind(this));
        this.denyAccessCommand.add(view.denyAccessButton);
        new TextBlock_1.TextBlock('Default Roles', view.defaultUserRolesTitle);
        this.defaultUserRoles = new ListGroup_1.ListGroup(view.defaultUserRoles);
    }
    UserRolesPanel.prototype.requestAdd = function () {
        this.awaitable.resolve(UserRolesPanelResult.addRequested());
    };
    UserRolesPanel.prototype.requestModifier = function () {
        this.awaitable.resolve(UserRolesPanelResult.modifierRequested());
    };
    UserRolesPanel.prototype.allowAccess = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Allowing Access...', function () { return _this.hubApi.AppUserMaintenance.AllowAccess({
                            UserID: _this.user.ID,
                            ModifierID: _this.modifier.ID
                        }); })];
                    case 1:
                        _a.sent();
                        return [4 /*yield*/, this.refresh()];
                    case 2:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    UserRolesPanel.prototype.denyAccess = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Denying Access...', function () { return _this.hubApi.AppUserMaintenance.DenyAccess({
                            UserID: _this.user.ID,
                            ModifierID: _this.modifier.ID
                        }); })];
                    case 1:
                        _a.sent();
                        return [4 /*yield*/, this.refresh()];
                    case 2:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    UserRolesPanel.prototype.setAppUserOptions = function (appUserOptions) {
        this.appName.setText(appUserOptions.app.AppName);
        this.appType.setText(appUserOptions.app.Type.DisplayText);
        this.userName.setText(appUserOptions.user.UserName);
        this.personName.setText(appUserOptions.user.Name);
        this.user = appUserOptions.user;
        this.defaultModifier = appUserOptions.defaultModifier;
    };
    UserRolesPanel.prototype.setDefaultModifier = function () {
        this.categoryName.setText('Default');
        this.modifierDisplayText.setText('');
        this.modifier = this.defaultModifier;
    };
    UserRolesPanel.prototype.setModCategory = function (modCategory) {
        this.categoryName.setText(modCategory.Name);
    };
    UserRolesPanel.prototype.setModifier = function (modifier) {
        this.modifierDisplayText.setText(modifier.DisplayText);
        this.modifier = modifier;
    };
    UserRolesPanel.prototype.start = function () {
        new DelayedAction_1.DelayedAction(this.refresh.bind(this), 1).execute();
        return this.awaitable.start();
    };
    UserRolesPanel.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var isDefaultModifier, userAccess, defaultUserAccess, _i, _a, listItem, userRoleListItems, _b, userRoleListItems_1, listItem;
            var _c;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_d) {
                switch (_d.label) {
                    case 0:
                        this.alert.clear();
                        this.allowAccessCommand.hide();
                        this.denyAccessCommand.hide();
                        this.addCommand.hide();
                        isDefaultModifier = this.modifier.ID === this.defaultModifier.ID;
                        return [4 /*yield*/, this.alert.infoAction('Loading', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                                return (0, tslib_1.__generator)(this, function (_a) {
                                    switch (_a.label) {
                                        case 0: return [4 /*yield*/, this.hubApi.AppUser.GetUserAccess({
                                                UserID: this.user.ID,
                                                ModifierID: this.modifier.ID
                                            })];
                                        case 1:
                                            userAccess = _a.sent();
                                            if (!isDefaultModifier) return [3 /*break*/, 2];
                                            defaultUserAccess = userAccess;
                                            return [3 /*break*/, 4];
                                        case 2: return [4 /*yield*/, this.hubApi.AppUser.GetUserAccess({
                                                UserID: this.user.ID,
                                                ModifierID: this.defaultModifier.ID
                                            })];
                                        case 3:
                                            defaultUserAccess = _a.sent();
                                            _a.label = 4;
                                        case 4: return [2 /*return*/];
                                    }
                                });
                            }); })];
                    case 1:
                        _d.sent();
                        if (userAccess.HasAccess) {
                            this.denyAccessCommand.show();
                            this.addCommand.show();
                        }
                        else {
                            this.allowAccessCommand.show();
                        }
                        this.deleteEvents.unregisterAll();
                        for (_i = 0, _a = this.userRoleListItems; _i < _a.length; _i++) {
                            listItem = _a[_i];
                            listItem.dispose();
                        }
                        userRoleListItems = this.userRoles.setItems(userAccess.AssignedRoles, function (role, itemView) {
                            return new UserRoleListItem_1.UserRoleListItem(role, itemView);
                        });
                        for (_b = 0, userRoleListItems_1 = userRoleListItems; _b < userRoleListItems_1.length; _b++) {
                            listItem = userRoleListItems_1[_b];
                            this.deleteEvents.register(listItem.deleteButtonClicked, this.onDeleteRoleClicked.bind(this));
                        }
                        (_c = this.userRoleListItems).splice.apply(_c, (0, tslib_1.__spreadArray)([0, this.userRoleListItems.length], userRoleListItems, false));
                        if (isDefaultModifier) {
                            this.view.hideDefaultUserRoles();
                        }
                        else {
                            this.view.showDefaultUserRoles();
                            this.defaultUserRoles.setItems(defaultUserAccess.AssignedRoles, function (role, itemView) {
                                var listItem = new UserRoleListItem_1.UserRoleListItem(role, itemView);
                                listItem.hideDeleteButton();
                                return listItem;
                            });
                        }
                        if (!userAccess.HasAccess) {
                            this.alert.danger('Access is denied.');
                        }
                        else if (userAccess.AssignedRoles.length === 0) {
                            this.alert.warning('No roles have been assigned.');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserRolesPanel.prototype.onDeleteRoleClicked = function (role) {
        var _this = this;
        return this.alert.infoAction('Removing role...', function () { return _this.hubApi.AppUserMaintenance.UnassignRole({
            UserID: _this.user.ID,
            ModifierID: _this.modifier.ID,
            RoleID: role.ID
        }); });
    };
    UserRolesPanel.prototype.activate = function () { this.view.show(); };
    UserRolesPanel.prototype.deactivate = function () { this.view.hide(); };
    return UserRolesPanel;
}());
exports.UserRolesPanel = UserRolesPanel;
//# sourceMappingURL=UserRolesPanel.js.map