"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AddRolePanel = exports.AddRolePanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var RoleListItem_1 = require("./RoleListItem");
var AddRolePanelResult = /** @class */ (function () {
    function AddRolePanelResult(results) {
        this.results = results;
    }
    AddRolePanelResult.back = function () { return new AddRolePanelResult({ back: {} }); };
    AddRolePanelResult.roleSelected = function () { return new AddRolePanelResult({ roleSelected: {} }); };
    Object.defineProperty(AddRolePanelResult.prototype, "back", {
        get: function () { return this.results.back; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AddRolePanelResult.prototype, "roleSelected", {
        get: function () { return this.results.roleSelected; },
        enumerable: false,
        configurable: true
    });
    return AddRolePanelResult;
}());
exports.AddRolePanelResult = AddRolePanelResult;
var AddRolePanel = /** @class */ (function () {
    function AddRolePanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Select Role', view.titleHeader);
        this.awaitable = new Awaitable_1.Awaitable();
        this.alert = new MessageAlert_1.MessageAlert(view.alert);
        this.roles = new ListGroup_1.ListGroup(view.roles);
        this.roles.itemClicked.register(this.onRoleClicked.bind(this));
    }
    AddRolePanel.prototype.onRoleClicked = function (roleListItem) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.addRole(roleListItem.role)];
                    case 1:
                        _a.sent();
                        this.awaitable.resolve(AddRolePanelResult.roleSelected());
                        return [2 /*return*/];
                }
            });
        });
    };
    AddRolePanel.prototype.setAppUserOptions = function (appUserOptions) {
        this.user = appUserOptions.user;
        this.defaultModifier = appUserOptions.defaultModifier;
    };
    AddRolePanel.prototype.setDefaultModifier = function () {
        this.setModifier(this.defaultModifier);
    };
    AddRolePanel.prototype.setModifier = function (modifier) {
        this.modifier = modifier;
    };
    AddRolePanel.prototype.addRole = function (role) {
        var _this = this;
        return this.alert.infoAction('Adding role...', function () { return _this.hubApi.AppUserMaintenance.AssignRole({
            UserID: _this.user.ID,
            ModifierID: _this.modifier.ID,
            RoleID: role.ID
        }); });
    };
    AddRolePanel.prototype.start = function () {
        this.roles.clearItems();
        new DelayedAction_1.DelayedAction(this.delayedStart.bind(this), 1).execute();
        return this.awaitable.start();
    };
    AddRolePanel.prototype.delayedStart = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var roles;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return _this.hubApi.AppUser.GetUnassignedRoles({
                            UserID: _this.user.ID,
                            ModifierID: _this.modifier.ID
                        }); })];
                    case 1:
                        roles = _a.sent();
                        this.roles.setItems(roles, function (role, view) { return new RoleListItem_1.RoleListItem(role, view); });
                        return [2 /*return*/];
                }
            });
        });
    };
    AddRolePanel.prototype.activate = function () { this.view.show(); };
    AddRolePanel.prototype.deactivate = function () { this.view.hide(); };
    return AddRolePanel;
}());
exports.AddRolePanel = AddRolePanel;
//# sourceMappingURL=AddRolePanel.js.map