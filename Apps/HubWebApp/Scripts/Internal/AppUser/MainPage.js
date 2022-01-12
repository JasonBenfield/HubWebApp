"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var SingleActivePanel_1 = require("@jasonbenfield/sharedwebapp/Panel/SingleActivePanel");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var Url_1 = require("@jasonbenfield/sharedwebapp/Url");
var WebPage_1 = require("@jasonbenfield/sharedwebapp/Api/WebPage");
var XtiUrl_1 = require("@jasonbenfield/sharedwebapp/Api/XtiUrl");
var Apis_1 = require("../Apis");
var AddRolePanel_1 = require("./AddRolePanel");
var AppUserDataPanel_1 = require("./AppUserDataPanel");
var MainPageView_1 = require("./MainPageView");
var SelectModCategoryPanel_1 = require("./SelectModCategoryPanel");
var SelectModifierPanel_1 = require("./SelectModifierPanel");
var UserRolesPanel_1 = require("./UserRolesPanel");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.view = new MainPageView_1.MainPageView(page);
        this.hubApi = new Apis_1.Apis(page.modalError).Hub();
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.appUserDataPanel = this.panels.add(new AppUserDataPanel_1.AppUserDataPanel(this.hubApi, this.view.appUserDataPanel));
        this.selectModCategoryPanel = this.panels.add(new SelectModCategoryPanel_1.SelectModCategoryPanel(this.hubApi, this.view.selectModCategoryPanel));
        this.selectModifierPanel = this.panels.add(new SelectModifierPanel_1.SelectModifierPanel(this.hubApi, this.view.selectModifierPanel));
        this.userRolesPanel = this.panels.add(new UserRolesPanel_1.UserRolesPanel(this.hubApi, this.view.userRolesPanel));
        this.addRolePanel = this.panels.add(new AddRolePanel_1.AddRolePanel(this.hubApi, this.view.addRolePanel));
        var userIDValue = Url_1.Url.current().getQueryValue('userID');
        if (XtiUrl_1.XtiUrl.current.path.modifier && userIDValue) {
            var userID = Number(userIDValue);
            this.activateStartPanel(userID);
        }
        else {
            new WebPage_1.WebPage(this.hubApi.Users.Index.getUrl({})).open();
        }
    }
    MainPage.prototype.activateStartPanel = function (userID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.appUserDataPanel);
                        return [4 /*yield*/, this.appUserDataPanel.start(userID)];
                    case 1:
                        result = _a.sent();
                        if (result.done) {
                            this.userRolesPanel.setAppUserOptions(result.done.appUserOptions);
                            this.userRolesPanel.setDefaultModifier();
                            this.addRolePanel.setAppUserOptions(result.done.appUserOptions);
                            this.addRolePanel.setDefaultModifier();
                            this.activateUserRolesPanel();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateSelectModCategoryPanel = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.selectModCategoryPanel);
                        return [4 /*yield*/, this.selectModCategoryPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.defaultModSelected) {
                            this.userRolesPanel.setDefaultModifier();
                            this.addRolePanel.setDefaultModifier();
                            this.activateUserRolesPanel();
                        }
                        else if (result.modCategorySelected) {
                            this.selectModifierPanel.setModCategory(result.modCategorySelected.modCategory);
                            this.userRolesPanel.setModCategory(result.modCategorySelected.modCategory);
                            this.activateSelectModifierPanel();
                        }
                        else if (result.back) {
                            this.activateUserRolesPanel();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateSelectModifierPanel = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.selectModifierPanel);
                        return [4 /*yield*/, this.selectModifierPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.modifierSelected) {
                            this.userRolesPanel.setModifier(result.modifierSelected.modifier);
                            this.addRolePanel.setModifier(result.modifierSelected.modifier);
                            this.activateUserRolesPanel();
                        }
                        else if (result.back) {
                            this.activateUserRolesPanel();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateUserRolesPanel = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.userRolesPanel);
                        return [4 /*yield*/, this.userRolesPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.addRequested) {
                            this.activateAddRolePanel();
                        }
                        else if (result.modifierRequested) {
                            this.activateSelectModCategoryPanel();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateAddRolePanel = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.addRolePanel);
                        return [4 /*yield*/, this.addRolePanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.back) {
                            this.activateUserRolesPanel();
                        }
                        else if (result.roleSelected) {
                            this.activateUserRolesPanel();
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