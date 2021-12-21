"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var SingleActivePanel_1 = require("@jasonbenfield/sharedwebapp/Panel/SingleActivePanel");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var Url_1 = require("@jasonbenfield/sharedwebapp/Url");
var WebPage_1 = require("@jasonbenfield/sharedwebapp/WebPage");
var XtiUrl_1 = require("@jasonbenfield/sharedwebapp/XtiUrl");
var Apis_1 = require("../../Hub/Apis");
var AppUserPanel_1 = require("./AppUser/AppUserPanel");
var MainPageView_1 = require("./MainPageView");
var UserRolePanel_1 = require("./UserRoles/UserRolePanel");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.view = new MainPageView_1.MainPageView(page);
        this.hubApi = new Apis_1.Apis(page.modalError).hub();
        this.appUserPanel = this.panels.add(new AppUserPanel_1.AppUserPanel(this.hubApi, this.view.appUserPanel));
        this.userRolePanel = this.panels.add(new UserRolePanel_1.UserRolePanel(this.hubApi, this.view.userRolePanel));
        var userID = Url_1.Url.current().getQueryValue('userID');
        if (XtiUrl_1.XtiUrl.current.path.modifier && userID) {
            this.activateAppUserPanel(Number(userID));
        }
        else {
            new WebPage_1.WebPage(this.hubApi.Users.Index.getUrl({})).open();
        }
    }
    MainPage.prototype.activateAppUserPanel = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.appUserPanel);
                        this.appUserPanel.setUserID(userID);
                        this.appUserPanel.refresh();
                        return [4 /*yield*/, this.appUserPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.hubApi.Users.Index.open({});
                        }
                        else if (result.editUserRolesRequested) {
                            this.activateUserRolePanel(userID);
                        }
                        else if (result.editUserModCategoryRequested) {
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserRolePanel = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userRolePanel);
                        this.userRolePanel.setUserID(userID);
                        this.userRolePanel.refresh();
                        return [4 /*yield*/, this.userRolePanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.activateAppUserPanel(userID);
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