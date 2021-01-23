"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("XtiShared/Result");
var Command_1 = require("XtiShared/Command");
var ResourceGroupAccessCard_1 = require("./ResourceGroupAccessCard");
var ResourceGroupComponent_1 = require("./ResourceGroupComponent");
var ResourceListCard_1 = require("./ResourceListCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var ModCategoryComponent_1 = require("./ModCategoryComponent");
var ResourceGroupPanel = /** @class */ (function () {
    function ResourceGroupPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.resourceGroupComponent = new ResourceGroupComponent_1.ResourceGroupComponent(this.vm.resourceGroupComponent, this.hubApi);
        this.modCategoryComponent = new ModCategoryComponent_1.ModCategoryComponent(this.vm.modCategoryComponent, this.hubApi);
        this.roleAccessCard = new ResourceGroupAccessCard_1.ResourceGroupAccessCard(this.vm.roleAccessCard, this.hubApi);
        this.resourceListCard = new ResourceListCard_1.ResourceListCard(this.vm.resourceListCard, this.hubApi);
        this.mostRecentRequestListCard = new MostRecentRequestListCard_1.MostRecentRequestListCard(this.vm.mostRecentRequestListCard, this.hubApi);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(this.vm.mostRecentErrorEventListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.vm.backCommand, this.back.bind(this));
        var icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('App');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
        this.modCategoryComponent.clicked.register(this.onModCategoryClicked.bind(this));
        this.resourceListCard.resourceSelected.register(this.onResourceSelected.bind(this));
    }
    ResourceGroupPanel.prototype.onModCategoryClicked = function (modCategory) {
        this.awaitable.resolve(new Result_1.Result(ResourceGroupPanel.ResultKeys.modCategorySelected, modCategory));
    };
    ResourceGroupPanel.prototype.onResourceSelected = function (resource) {
        this.awaitable.resolve(new Result_1.Result(ResourceGroupPanel.ResultKeys.resourceSelected, resource));
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
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var tasks;
            return tslib_1.__generator(this, function (_a) {
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
        this.awaitable.resolve(new Result_1.Result(ResourceGroupPanel.ResultKeys.backRequested));
    };
    ResourceGroupPanel.ResultKeys = {
        backRequested: 'back-requested',
        resourceSelected: 'resource-selected',
        modCategorySelected: 'mod-category-selected'
    };
    return ResourceGroupPanel;
}());
exports.ResourceGroupPanel = ResourceGroupPanel;
//# sourceMappingURL=ResourceGroupPanel.js.map