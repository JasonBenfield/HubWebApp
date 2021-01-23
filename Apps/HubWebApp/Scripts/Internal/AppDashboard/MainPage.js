"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
require("reflect-metadata");
var xtistart_1 = require("xtistart");
var tsyringe_1 = require("tsyringe");
var MainPageViewModel_1 = require("./MainPageViewModel");
var HubAppApi_1 = require("../../Hub/Api/HubAppApi");
var XtiUrl_1 = require("XtiShared/XtiUrl");
var WebPage_1 = require("XtiShared/WebPage");
var SingleActivePanel_1 = require("../Panel/SingleActivePanel");
var AppDetailPanel_1 = require("./AppDetail/AppDetailPanel");
var ResourceGroupPanel_1 = require("./ResourceGroup/ResourceGroupPanel");
var ResourcePanel_1 = require("./Resource/ResourcePanel");
var ModCategoryPanel_1 = require("./ModCategory/ModCategoryPanel");
var MainPage = /** @class */ (function () {
    function MainPage(vm, hubApi) {
        var _this = this;
        this.vm = vm;
        this.hubApi = hubApi;
        this.panels = new SingleActivePanel_1.SingleActivePanel();
        this.appDetailPanel = this.panels.add(this.vm.appDetailPanel, function (vm) { return new AppDetailPanel_1.AppDetailPanel(vm, _this.hubApi); });
        this.resourceGroupPanel = this.panels.add(this.vm.resourceGroupPanel, function (vm) { return new ResourceGroupPanel_1.ResourceGroupPanel(vm, _this.hubApi); });
        this.resourcePanel = this.panels.add(this.vm.resourcePanel, function (vm) { return new ResourcePanel_1.ResourcePanel(vm, _this.hubApi); });
        this.modCategoryPanel = this.panels.add(this.vm.modCategoryPanel, function (vm) { return new ModCategoryPanel_1.ModCategoryPanel(vm, _this.hubApi); });
        if (XtiUrl_1.XtiUrl.current.path.modifier) {
            this.activateAppDetailPanel();
        }
        else {
            new WebPage_1.WebPage(this.hubApi.Apps.Index.getUrl({})).open();
        }
    }
    MainPage.prototype.activateAppDetailPanel = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result, resourceGroup, modCategory;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.appDetailPanel);
                        this.appDetailPanel.content.refresh();
                        return [4 /*yield*/, this.appDetailPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === AppDetailPanel_1.AppDetailPanel.ResultKeys.backRequested) {
                            this.hubApi.Apps.Index.open({});
                        }
                        else if (result.key === AppDetailPanel_1.AppDetailPanel.ResultKeys.resourceGroupSelected) {
                            resourceGroup = result.data;
                            this.activateResourceGroupPanel(resourceGroup.ID);
                        }
                        else if (result.key === AppDetailPanel_1.AppDetailPanel.ResultKeys.modCategorySelected) {
                            modCategory = result.data;
                            this.activateModCategoryPanel(modCategory.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateResourceGroupPanel = function (groupID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result, resource, modCategory;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.resourceGroupPanel);
                        if (groupID) {
                            this.resourceGroupPanel.content.setGroupID(groupID);
                        }
                        this.resourceGroupPanel.content.refresh();
                        return [4 /*yield*/, this.resourceGroupPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === ResourceGroupPanel_1.ResourceGroupPanel.ResultKeys.backRequested) {
                            this.activateAppDetailPanel();
                        }
                        else if (result.key === ResourceGroupPanel_1.ResourceGroupPanel.ResultKeys.resourceSelected) {
                            resource = result.data;
                            this.activateResourcePanel(resource.ID);
                        }
                        else if (result.key === ResourceGroupPanel_1.ResourceGroupPanel.ResultKeys.modCategorySelected) {
                            modCategory = result.data;
                            this.activateModCategoryPanel(modCategory.ID);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateResourcePanel = function (resourceID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.resourcePanel);
                        if (resourceID) {
                            this.resourcePanel.content.setResourceID(resourceID);
                        }
                        this.resourcePanel.content.refresh();
                        return [4 /*yield*/, this.resourcePanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === ResourcePanel_1.ResourcePanel.ResultKeys.backRequested) {
                            this.activateResourceGroupPanel();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    MainPage.prototype.activateModCategoryPanel = function (modCategoryID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var result, resourceGroup;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.panels.activate(this.modCategoryPanel);
                        this.modCategoryPanel.content.setModCategoryID(modCategoryID);
                        this.modCategoryPanel.content.refresh();
                        return [4 /*yield*/, this.modCategoryPanel.content.start()];
                    case 1:
                        result = _a.sent();
                        if (result.key === ModCategoryPanel_1.ModCategoryPanel.ResultKeys.backRequested) {
                            this.activateAppDetailPanel();
                        }
                        else if (result.key === ModCategoryPanel_1.ModCategoryPanel.ResultKeys.resourceGroupSelected) {
                            resourceGroup = result.data;
                            this.activateResourceGroupPanel(resourceGroup.ID);
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