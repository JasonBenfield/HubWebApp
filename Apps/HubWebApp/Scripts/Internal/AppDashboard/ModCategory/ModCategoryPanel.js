"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryPanel = exports.ModCategoryPanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var ModCategoryComponent_1 = require("./ModCategoryComponent");
var ModifierListCard_1 = require("./ModifierListCard");
var ResourceGroupListCard_1 = require("./ResourceGroupListCard");
var ModCategoryPanelResult = /** @class */ (function () {
    function ModCategoryPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(ModCategoryPanelResult, "backRequested", {
        get: function () {
            return new ModCategoryPanelResult({ backRequested: {} });
        },
        enumerable: false,
        configurable: true
    });
    ModCategoryPanelResult.resourceGroupSelected = function (resourceGroup) {
        return new ModCategoryPanelResult({
            resourceGroupSelected: { resourceGroup: resourceGroup }
        });
    };
    Object.defineProperty(ModCategoryPanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ModCategoryPanelResult.prototype, "resourceGroupSelected", {
        get: function () { return this.results.resourceGroupSelected; },
        enumerable: false,
        configurable: true
    });
    return ModCategoryPanelResult;
}());
exports.ModCategoryPanelResult = ModCategoryPanelResult;
var ModCategoryPanel = /** @class */ (function () {
    function ModCategoryPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        this.modCategoryComponent = new ModCategoryComponent_1.ModCategoryComponent(this.hubApi, this.view.modCategoryComponent);
        this.modifierListCard = new ModifierListCard_1.ModifierListCard(this.hubApi, this.view.modifierListCard);
        this.resourceGroupListCard = new ResourceGroupListCard_1.ResourceGroupListCard(this.hubApi, this.view.resourceGroupListCard);
        this.backCommand.add(this.view.backButton);
        this.resourceGroupListCard.resourceGroupSelected.register(this.onResourceGroupSelected.bind(this));
    }
    ModCategoryPanel.prototype.onResourceGroupSelected = function (resourceGroup) {
        this.awaitable.resolve(ModCategoryPanelResult.resourceGroupSelected(resourceGroup));
    };
    ModCategoryPanel.prototype.setModCategoryID = function (categoryID) {
        this.modCategoryComponent.setModCategoryID(categoryID);
        this.modifierListCard.setModCategoryID(categoryID);
        this.resourceGroupListCard.setModCategoryID(categoryID);
    };
    ModCategoryPanel.prototype.refresh = function () {
        var promises = [
            this.modCategoryComponent.refresh(),
            this.modifierListCard.refresh(),
            this.resourceGroupListCard.refresh()
        ];
        return Promise.all(promises);
    };
    ModCategoryPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    ModCategoryPanel.prototype.back = function () {
        this.awaitable.resolve(ModCategoryPanelResult.backRequested);
    };
    ModCategoryPanel.prototype.activate = function () { this.view.show(); };
    ModCategoryPanel.prototype.deactivate = function () { this.view.hide(); };
    return ModCategoryPanel;
}());
exports.ModCategoryPanel = ModCategoryPanel;
//# sourceMappingURL=ModCategoryPanel.js.map