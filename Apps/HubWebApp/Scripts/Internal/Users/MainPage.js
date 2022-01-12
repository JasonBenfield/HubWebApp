"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var SingleActivePanel_1 = require("@jasonbenfield/sharedwebapp/Panel/SingleActivePanel");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var Apis_1 = require("../Apis");
var MainPageView_1 = require("./MainPageView");
var UserPanel_1 = require("./User/UserPanel");
var UserEditPanel_1 = require("./UserEdit/UserEditPanel");
var UserListPanel_1 = require("./UserList/UserListPanel");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.view = new MainPageView_1.MainPageView(page);
        this.hubApi = new Apis_1.Apis(page.modalError).Hub();
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.userListPanel = this.panels.add(new UserListPanel_1.UserListPanel(this.hubApi, this.view.userListPanel));
        this.userPanel = this.panels.add(new UserPanel_1.UserPanel(this.hubApi, this.view.userPanel));
        this.userEditPanel = this.panels.add(new UserEditPanel_1.UserEditPanel(this.hubApi, this.view.userEditPanel));
        this.activateUserListPanel();
    }
    MainPage.prototype.activateUserListPanel = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userListPanel);
                        this.userListPanel.refresh();
                        return [4 /*yield*/, this.userListPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.userSelected) {
                            this.activateUserPanel(result.userSelected.user.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserPanel = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userPanel);
                        this.userPanel.setUserID(userID);
                        this.userPanel.refresh();
                        return [4 /*yield*/, this.userPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.activateUserListPanel();
                        }
                        else if (result.editRequested) {
                            this.activateUserEditPanel(userID);
                        }
                        else if (result.appSelected) {
                            this.hubApi.UserInquiry.RedirectToAppUser.open({
                                UserID: userID,
                                AppID: result.appSelected.app.ID
                            });
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserEditPanel = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userEditPanel);
                        this.userEditPanel.setUserID(userID);
                        this.userEditPanel.refresh();
                        return [4 /*yield*/, this.userEditPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.canceled || result.saved) {
                            this.activateUserPanel(userID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    return MainPage;
}());
new MainPage(new Startup_1.Startup().build());
//# sourceMappingURL=MainPage.js.map