"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRolesPanel = exports.UserRolesPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
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
        this.alert = new MessageAlert_1.MessageAlert(view.alert);
        this.userRoles = new ListGroup_1.ListGroup(view.userRoles);
        this.awaitable = new Awaitable_1.Awaitable();
        new Command_1.Command(this.requestAdd.bind(this)).add(view.addButton);
        new Command_1.Command(this.requestModifier.bind(this)).add(view.selectModifierButton);
    }
    UserRolesPanel.prototype.requestAdd = function () {
        this.awaitable.resolve(UserRolesPanelResult.addRequested());
    };
    UserRolesPanel.prototype.requestModifier = function () {
        this.awaitable.resolve(UserRolesPanelResult.modifierRequested());
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
        new DelayedAction_1.DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    };
    UserRolesPanel.prototype.delayedStart = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var userRoles, _i, _a, listItem, userRoleListItems, _b, userRoleListItems_1, listItem;
            var _c;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_d) {
                switch (_d.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading', function () { return _this.hubApi.AppUser.GetUserRoles({
                            UserID: _this.user.ID,
                            ModifierID: _this.modifier.ID
                        }); })];
                    case 1:
                        userRoles = _d.sent();
                        this.deleteEvents.unregisterAll();
                        for (_i = 0, _a = this.userRoleListItems; _i < _a.length; _i++) {
                            listItem = _a[_i];
                            listItem.dispose();
                        }
                        userRoleListItems = this.userRoles.setItems(userRoles, function (role, itemView) {
                            return new UserRoleListItem_1.UserRoleListItem(role, itemView);
                        });
                        for (_b = 0, userRoleListItems_1 = userRoleListItems; _b < userRoleListItems_1.length; _b++) {
                            listItem = userRoleListItems_1[_b];
                            this.deleteEvents.register(listItem.deleteButtonClicked, this.onDeleteRoleClicked.bind(this));
                        }
                        (_c = this.userRoleListItems).splice.apply(_c, (0, tslib_1.__spreadArray)([0, this.userRoleListItems.length], userRoleListItems, false));
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