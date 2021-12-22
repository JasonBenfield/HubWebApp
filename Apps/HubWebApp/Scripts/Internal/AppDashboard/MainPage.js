"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var WebPage_1 = require("@jasonbenfield/sharedwebapp/WebPage");
var XtiUrl_1 = require("@jasonbenfield/sharedwebapp/XtiUrl");
var Apis_1 = require("../../Hub/Apis");
var AppDetailPanel_1 = require("./AppDetail/AppDetailPanel");
var MainPageView_1 = require("./MainPageView");
var ModCategoryPanel_1 = require("./ModCategory/ModCategoryPanel");
var ResourcePanel_1 = require("./Resource/ResourcePanel");
var ResourceGroupPanel_1 = require("./ResourceGroup/ResourceGroupPanel");
var MainPage = /** @class */ (function () {
    function MainPage(page) {
        this.view = new MainPageView_1.MainPageView(page);
        this.hubApi = new Apis_1.Apis(page.modalError).hub();
        this.appDetailPanel = this.panels.add(new AppDetailPanel_1.AppDetailPanel(this.hubApi, this.view.appDetailPanel));
        this.resourceGroupPanel = this.panels.add(new ResourceGroupPanel_1.ResourceGroupPanel(this.hubApi, this.view.resourceGroupPanel));
        this.resourcePanel = this.panels.add(new ResourcePanel_1.ResourcePanel(this.hubApi, this.view.resourcePanel));
        this.modCategoryPanel = this.panels.add(new ModCategoryPanel_1.ModCategoryPanel(this.hubApi, this.view.modCategoryPanel));
        if (XtiUrl_1.XtiUrl.current.path.modifier) {
            this.activateAppDetailPanel();
        }
        else {
            new WebPage_1.WebPage(this.hubApi.Apps.Index.getUrl({})).open();
        }
    }
    MainPage.prototype.activateAppDetailPanel = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.appDetailPanel);
                        this.appDetailPanel.refresh();
                        return [4 /*yield*/, this.appDetailPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.hubApi.Apps.Index.open({});
                        }
                        else if (result.resourceGroupSelected) {
                            this.activateResourceGroupPanel(result.resourceGroupSelected.resourceGroup.ID);
                        }
                        else if (result.modCategorySelected) {
                            this.activateModCategoryPanel(result.modCategorySelected.modCategory.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateResourceGroupPanel = function (groupID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.resourceGroupPanel);
                        if (groupID) {
                            this.resourceGroupPanel.setGroupID(groupID);
                        }
                        this.resourceGroupPanel.refresh();
                        return [4 /*yield*/, this.resourceGroupPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.activateAppDetailPanel();
                        }
                        else if (result.resourceSelected) {
                            this.activateResourcePanel(result.resourceSelected.resource.ID);
                        }
                        else if (result.modCategorySelected) {
                            this.activateModCategoryPanel(result.modCategorySelected.modCategory.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateResourcePanel = function (resourceID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.resourcePanel);
                        if (resourceID) {
                            this.resourcePanel.setResourceID(resourceID);
                        }
                        this.resourcePanel.refresh();
                        return [4 /*yield*/, this.resourcePanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.activateResourceGroupPanel();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateModCategoryPanel = function (modCategoryID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var result;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.modCategoryPanel);
                        this.modCategoryPanel.setModCategoryID(modCategoryID);
                        this.modCategoryPanel.refresh();
                        return [4 /*yield*/, this.modCategoryPanel.start()];
                    case 1:
                        result = _a.sent();
                        if (result.backRequested) {
                            this.activateAppDetailPanel();
                        }
                        else if (result.resourceGroupSelected) {
                            this.activateResourceGroupPanel(result.resourceGroupSelected.resourceGroup.ID);
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