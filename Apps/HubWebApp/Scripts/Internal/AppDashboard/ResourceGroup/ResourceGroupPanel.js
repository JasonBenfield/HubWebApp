"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupPanel = exports.ResourceGroupPanelResult = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var ModCategoryComponent_1 = require("./ModCategoryComponent");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var ResourceGroupAccessCard_1 = require("./ResourceGroupAccessCard");
var ResourceGroupComponent_1 = require("./ResourceGroupComponent");
var ResourceListCard_1 = require("./ResourceListCard");
var ResourceGroupPanelResult = /** @class */ (function () {
    function ResourceGroupPanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(ResourceGroupPanelResult, "backRequested", {
        get: function () { return new ResourceGroupPanelResult({ backRequested: {} }); },
        enumerable: false,
        configurable: true
    });
    ResourceGroupPanelResult.resourceSelected = function (resource) {
        return new ResourceGroupPanelResult({
            resourceSelected: { resource: resource }
        });
    };
    ResourceGroupPanelResult.modCategorySelected = function (modCategory) {
        return new ResourceGroupPanelResult({
            modCategorySelected: { modCategory: modCategory }
        });
    };
    Object.defineProperty(ResourceGroupPanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ResourceGroupPanelResult.prototype, "resourceSelected", {
        get: function () { return this.results.resourceSelected; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ResourceGroupPanelResult.prototype, "modCategorySelected", {
        get: function () { return this.results.modCategorySelected; },
        enumerable: false,
        configurable: true
    });
    return ResourceGroupPanelResult;
}());
exports.ResourceGroupPanelResult = ResourceGroupPanelResult;
var ResourceGroupPanel = /** @class */ (function () {
    function ResourceGroupPanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.backCommand = new Command_1.Command(this.back.bind(this));
        this.awaitable = new Awaitable_1.Awaitable();
        this.resourceGroupComponent = new ResourceGroupComponent_1.ResourceGroupComponent(this.hubApi, this.view.resourceGroupComponent);
        this.modCategoryComponent = new ModCategoryComponent_1.ModCategoryComponent(this.hubApi, this.view.modCategoryComponent);
        this.modCategoryComponent.clicked.register(this.onModCategoryClicked.bind(this));
        this.roleAccessCard = new ResourceGroupAccessCard_1.ResourceGroupAccessCard(this.hubApi, this.view.roleAccessCard);
        this.resourceListCard = new ResourceListCard_1.ResourceListCard(this.hubApi, this.view.resourceListCard);
        this.resourceListCard.resourceSelected.register(this.onResourceSelected.bind(this));
        this.mostRecentRequestListCard = new MostRecentRequestListCard_1.MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);
        this.backCommand.add(this.view.backButton);
    }
    ResourceGroupPanel.prototype.onModCategoryClicked = function (modCategory) {
        this.awaitable.resolve(ResourceGroupPanelResult.modCategorySelected(modCategory));
    };
    ResourceGroupPanel.prototype.onResourceSelected = function (resource) {
        this.awaitable.resolve(ResourceGroupPanelResult.resourceSelected(resource));
    };
    ResourceGroupPanel.prototype.setGroupID = function (groupID) {
        this.resourceGroupComponent.setGroupID(groupID);
        this.modCategoryComponent.setGroupID(groupID);
        this.roleAccessCard.setGroupID(groupID);
        this.resourceListCard.setGroupID(groupID);
        this.mostRecentRequestListCard.setGroupID(groupID);
        this.mostRecentErrorEventListCard.setGroupID(groupID);
    };
    ResourceGroupPanel.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var tasks;
            return (0, tslib_1.__generator)(this, function (_a) {
                tasks = [
                    this.resourceGroupComponent.refresh(),
                    this.modCategoryComponent.refresh(),
                    this.roleAccessCard.refresh(),
                    this.resourceListCard.refresh(),
                    this.mostRecentRequestListCard.refresh(),
                    this.mostRecentErrorEventListCard.refresh()
                ];
                return [2 /*return*/, Promise.all(tasks)];
            });
        });
    };
    ResourceGroupPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    ResourceGroupPanel.prototype.back = function () {
        this.awaitable.resolve(ResourceGroupPanelResult.backRequested);
    };
    ResourceGroupPanel.prototype.activate = function () { this.view.show(); };
    ResourceGroupPanel.prototype.deactivate = function () { this.view.hide(); };
    ResourceGroupPanel.ResultKeys = {
        backRequested: 'back-requested',
        resourceSelected: 'resource-selected',
        modCategorySelected: 'mod-category-selected'
    };
    return ResourceGroupPanel;
}());
exports.ResourceGroupPanel = ResourceGroupPanel;
//# sourceMappingURL=ResourceGroupPanel.js.map