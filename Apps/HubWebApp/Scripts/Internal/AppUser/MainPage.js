"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var xtistart_1 = require("xtistart");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var XtiUrl_1 = require("XtiShared/XtiUrl");
var WebPage_1 = require("XtiShared/WebPage");
var SingleActivePanel_1 = require("../Panel/SingleActivePanel");
var Url_1 = require("XtiShared/Url");
var AppUserPanel_1 = require("./AppUser/AppUserPanel");
var PaddingCss_1 = require("XtiShared/PaddingCss");
var UserRolePanel_1 = require("./UserRoles/UserRolePanel");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.page = page;
        this.hubApi = this.page.api(HubAppApi_1.HubAppApi);
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.appUserPanel = this.page.addContent(this.panels.add(new AppUserPanel_1.AppUserPanel(this.hubApi)));
        this.userRolePanel = this.page.addContent(this.panels.add(new UserRolePanel_1.UserRolePanel(this.hubApi)));
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
        var userID = Url_1.Url.current().getQueryValue('userID');
        if (XtiUrl_1.XtiUrl.current.path.modifier && userID) {
            this.activateAppUserPanel(Number(userID));
        }
        else {
            new WebPage_1.WebPage(this.hubApi.Users.Index.getUrl({})).open();
        }
    }
    MainPage.prototype.activateAppUserPanel = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result, userModCategory;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.appUserPanel);
                        this.appUserPanel.content.setUserID(userID);
                        this.appUserPanel.content.refresh();
                        return [4 /*yield*/, this.appUserPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === AppUserPanel_1.AppUserPanel.ResultKeys.backRequested) {
                            this.hubApi.Users.Index.open({});
                        }
                        else if (result.key === AppUserPanel_1.AppUserPanel.ResultKeys.editUserRolesRequested) {
                            this.activateUserRolePanel(userID);
                        }
                        else if (result.key === AppUserPanel_1.AppUserPanel.ResultKeys.editUserModCategoryRequested) {
                            userModCategory = result.data;
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserRolePanel = function (userID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userRolePanel);
                        this.userRolePanel.content.setUserID(userID);
                        this.userRolePanel.content.refresh();
                        return [4 /*yield*/, this.userRolePanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === AppUserPanel_1.AppUserPanel.ResultKeys.backRequested) {
                            this.activateAppUserPanel(userID);
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