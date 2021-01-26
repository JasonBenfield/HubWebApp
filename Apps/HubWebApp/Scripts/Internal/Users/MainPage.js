"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
require("reflect-metadata");
var xtistart_1 = require("xtistart");
var tsyringe_1 = require("tsyringe");
var MainPageViewModel_1 = require("./MainPageViewModel");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var UserListPanel_1 = require("./UserList/UserListPanel");
var SingleActivePanel_1 = require("../Panel/SingleActivePanel");
var UserPanel_1 = require("./User/UserPanel");
var UserEditPanel_1 = require("./UserEdit/UserEditPanel");
var MainPage = /** @class */ (function () {
    function MainPage(vm, hubApi) {
        var _this = this;
        this.vm = vm;
        this.hubApi = hubApi;
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.userListPanel = this.panels.add(this.vm.userListPanel, function (vm) { return new UserListPanel_1.UserListPanel(vm, _this.hubApi); });
        this.userPanel = this.panels.add(this.vm.userPanel, function (vm) { return new UserPanel_1.UserPanel(vm, _this.hubApi); });
        this.userEditPanel = this.panels.add(this.vm.userEditPanel, function (vm) { return new UserEditPanel_1.UserEditPanel(vm, _this.hubApi); });
        this.activateUserListPanel();
    }
    MainPage.prototype.activateUserListPanel = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result, user;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userListPanel);
                        this.userListPanel.content.refresh();
                        return [4 /*yield*/, this.userListPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === UserListPanel_1.UserListPanel.ResultKeys.userSelected) {
                            user = result.data;
                            this.activateUserPanel(user.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserPanel = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userPanel);
                        this.userPanel.content.setUserID(userID);
                        this.userPanel.content.refresh();
                        return [4 /*yield*/, this.userPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === UserPanel_1.UserPanel.ResultKeys.backRequested) {
                            this.activateUserListPanel();
                        }
                        else if (result.key === UserPanel_1.UserPanel.ResultKeys.editRequested) {
                            this.activateUserEditPanel(userID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserEditPanel = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userEditPanel);
                        this.userEditPanel.content.setUserID(userID);
                        this.userEditPanel.content.refresh();
                        return [4 /*yield*/, this.userEditPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === UserEditPanel_1.UserEditPanel.ResultKeys.canceled ||
                            result.key === UserEditPanel_1.UserEditPanel.ResultKeys.saved) {
                            this.activateUserPanel(userID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage = tslib_1.__decorate([
        tsyringe_1.singleton(),
        tslib_1.__metadata("design:paramtypes", [MainPageViewModel_1.MainPageViewModel,
            HubAppApi_1.HubAppApi])
    ], MainPage);
    return MainPage;
}());
xtistart_1.startup(MainPageViewModel_1.MainPageViewModel, MainPage);
//# sourceMappingURL=MainPage.js.map