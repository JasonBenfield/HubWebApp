"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserEditPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command");
var Result_1 = require("XtiShared/Result");
var EditUserForm_1 = require("../../../Hub/Api/EditUserForm");
var Alert_1 = require("XtiShared/Alert");
var DelayedAction_1 = require("XtiShared/DelayedAction");
var UserEditPanel = /** @class */ (function () {
    function UserEditPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.editUserForm = new EditUserForm_1.EditUserForm(this.vm.editUserForm);
        this.alert = new Alert_1.Alert(this.vm.alert);
        this.awaitable = new Awaitable_1.Awaitable();
        this.cancelCommand = new Command_1.Command(this.vm.cancelCommand, this.cancel.bind(this));
        this.saveCommand = new Command_1.AsyncCommand(this.vm.saveCommand, this.save.bind(this));
        var cancelIcon = this.cancelCommand.icon();
        cancelIcon.setName('fa-times');
        this.cancelCommand.setText('Cancel');
        this.cancelCommand.makeDanger();
        var saveIcon = this.saveCommand.icon();
        saveIcon.setName('fa-check');
        this.saveCommand.setText('Save');
        this.saveCommand.makePrimary();
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
                        new DelayedAction_1.DelayedAction(function () { return _this.editUserForm.PersonName.setFocus(); }, 1000).execute();
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
        this.awaitable.resolve(new Result_1.Result(UserEditPanel.ResultKeys.canceled));
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
                            this.awaitable.resolve(new Result_1.Result(UserEditPanel.ResultKeys.saved));
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserEditPanel.ResultKeys = {
        canceled: 'canceled',
        saved: 'saved'
    };
    return UserEditPanel;
}());
exports.UserEditPanel = UserEditPanel;
//# sourceMappingURL=UserEditPanel.js.map