"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserEditPanel = exports.UserEditPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var AsyncCommand_1 = require("@jasonbenfield/sharedwebapp/Command/AsyncCommand");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var DelayedAction_1 = require("@jasonbenfield/sharedwebapp/DelayedAction");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var EditUserForm_1 = require("../../../Hub/Api/EditUserForm");
var UserEditPanelResult = /** @class */ (function () {
    function UserEditPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(UserEditPanelResult, "canceled", {
        get: function () { return new UserEditPanelResult({ canceled: {} }); },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserEditPanelResult, "saved", {
        get: function () { return new UserEditPanelResult({ saved: {} }); },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserEditPanelResult.prototype, "canceled", {
        get: function () { return this.results.canceled; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(UserEditPanelResult.prototype, "saved", {
        get: function () { return this.results.saved; },
        enumerable: false,
        configurable: true
    });
    return UserEditPanelResult;
}());
exports.UserEditPanelResult = UserEditPanelResult;
var UserEditPanel = /** @class */ (function () {
    function UserEditPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.cancelCommand = new Command_1.Command(this.cancel.bind(this));
        this.saveCommand = new AsyncCommand_1.AsyncCommand(this.save.bind(this));
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.cancelCommand.add(this.view.cancelButton);
        this.saveCommand.add(this.view.saveButton);
        new TextBlock_1.TextBlock('Edit User', this.view.titleHeader);
        this.editUserForm = new EditUserForm_1.EditUserForm(this.view.editUserForm);
    }
    UserEditPanel.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserEditPanel.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var userForm;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUserForEdit(this.userID)];
                    case 1:
                        userForm = _a.sent();
                        this.editUserForm.import(userForm);
                        return [4 /*yield*/, new DelayedAction_1.DelayedAction(function () { return _this.editUserForm.PersonName.setFocus(); }, 1000).execute()];
                    case 2:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    UserEditPanel.prototype.getUserForEdit = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var userForm;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.UserMaintenance.GetUserForEdit(userID)];
                                    case 1:
                                        userForm = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, userForm];
                }
            });
        });
    };
    UserEditPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    UserEditPanel.prototype.cancel = function () {
        this.awaitable.resolve(UserEditPanelResult.canceled);
    };
    UserEditPanel.prototype.save = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.editUserForm.save(this.hubApi.UserMaintenance.EditUserAction)];
                    case 1:
                        result = _a.sent();
                        if (result.succeeded()) {
                            this.awaitable.resolve(UserEditPanelResult.saved);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserEditPanel.prototype.activate = function () { this.view.show(); };
    UserEditPanel.prototype.deactivate = function () { this.view.hide(); };
    return UserEditPanel;
}());
exports.UserEditPanel = UserEditPanel;
//# sourceMappingURL=UserEditPanel.js.map