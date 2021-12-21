"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppDetailPanel = exports.AppDetailPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var AppComponent_1 = require("./AppComponent");
var CurrentVersionComponent_1 = require("./CurrentVersionComponent");
var ModifierCategoryListCard_1 = require("./ModifierCategoryListCard");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var ResourceGroupListCard_1 = require("./ResourceGroupListCard");
var AppDetailPanelResult = /** @class */ (function () {
    function AppDetailPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(AppDetailPanelResult, "backRequested", {
        get: function () {
            return new AppDetailPanelResult({ backRequested: {} });
        },
        enumerable: false,
        configurable: true
    });
    AppDetailPanelResult.resourceGroupSelected = function (resourceGroup) {
        return new AppDetailPanelResult({
            resourceGroupSelected: { resourceGroup: resourceGroup }
        });
    };
    AppDetailPanelResult.modCategorySelected = function (modCategory) {
        return new AppDetailPanelResult({
            modCategorySelected: { modCategory: modCategory }
        });
    };
    Object.defineProperty(AppDetailPanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AppDetailPanelResult.prototype, "resourceGroupSelected", {
        get: function () { return this.results.resourceGroupSelected; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AppDetailPanelResult.prototype, "modCategorySelected", {
        get: function () { return this.results.modCategorySelected; },
        enumerable: false,
        configurable: true
    });
    return AppDetailPanelResult;
}());
exports.AppDetailPanelResult = AppDetailPanelResult;
var AppDetailPanel = /** @class */ (function () {
    function AppDetailPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        this.app = new AppComponent_1.AppComponent(this.hubApi, this.view.app);
        this.currentVersion = new CurrentVersionComponent_1.CurrentVersionComponent(this.hubApi, this.view.currentVersion);
        this.resourceGroupListCard = new ResourceGroupListCard_1.ResourceGroupListCard(this.hubApi, this.view.resourceGroupListCard);
        this.resourceGroupListCard.resourceGroupSelected.register(this.onResourceGroupSelected.bind(this));
        this.modifierCategoryListCard = new ModifierCategoryListCard_1.ModifierCategoryListCard(this.hubApi, this.view.modifierCategoryListCard);
        this.modifierCategoryListCard.modCategorySelected.register(this.onModCategorySelected.bind(this));
        this.mostRecentRequestListCard = new MostRecentRequestListCard_1.MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);
        this.backCommand.add(this.view.backButton);
    }
    AppDetailPanel.prototype.onResourceGroupSelected = function (resourceGroup) {
        this.awaitable.resolve(AppDetailPanelResult.resourceGroupSelected(resourceGroup));
    };
    AppDetailPanel.prototype.onModCategorySelected = function (modCategory) {
        this.awaitable.resolve(AppDetailPanelResult.modCategorySelected(modCategory));
    };
    AppDetailPanel.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var promises;
            return (0, tslib_1.__generator)(this, function (_a) {
                promises = [
                    this.app.refresh(),
                    this.currentVersion.refresh(),
                    this.resourceGroupListCard.refresh(),
                    this.modifierCategoryListCard.refresh(),
                    this.mostRecentRequestListCard.refresh(),
                    this.mostRecentErrorEventListCard.refresh()
                ];
                return [2 /*return*/, Promise.all(promises)];
            });
        });
    };
    AppDetailPanel.prototype.back = function () {
        this.awaitable.resolve(AppDetailPanelResult.backRequested);
    };
    AppDetailPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    AppDetailPanel.prototype.activate = function () { this.view.show(); };
    AppDetailPanel.prototype.deactivate = function () { this.view.hide(); };
    return AppDetailPanel;
}());
exports.AppDetailPanel = AppDetailPanel;
//# sourceMappingURL=AppDetailPanel.js.map