"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var xtistart_1 = require("xtistart");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var UserListPanel_1 = require("./UserList/UserListPanel");
var SingleActivePanel_1 = require("../Panel/SingleActivePanel");
var UserPanel_1 = require("./User/UserPanel");
var UserEditPanel_1 = require("./UserEdit/UserEditPanel");
var PaddingCss_1 = require("XtiShared/PaddingCss");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.page = page;
        this.hubApi = this.page.api(HubAppApi_1.HubAppApi);
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.userListPanel = this.page.addContent(this.panels.add(new UserListPanel_1.UserListPanel(this.hubApi)));
        this.userPanel = this.page.addContent(this.panels.add(new UserPanel_1.UserPanel(this.hubApi)));
        this.userEditPanel = this.page.addContent(this.panels.add(new UserEditPanel_1.UserEditPanel(this.hubApi)));
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
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
            var result, app;
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
                        else if (result.key === UserPanel_1.UserPanel.ResultKeys.appSelected) {
                            app = result.data;
                            this.hubApi.UserInquiry.RedirectToAppUser.open({ UserID: userID, AppID: app.ID });
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
    return MainPage;
}());
new MainPage(new xtistart_1.Startup().build());
//# sourceMappingURL=MainPage.js.map