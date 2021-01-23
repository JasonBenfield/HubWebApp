"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppDetailPanel = void 0;
var tslib_1 = require("tslib");
var AppComponent_1 = require("./AppComponent");
var CurrentVersionComponent_1 = require("./CurrentVersionComponent");
var ResourceGroupListCard_1 = require("./ResourceGroupListCard");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var ModifierCategoryListCard_1 = require("./ModifierCategoryListCard");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command");
var Result_1 = require("XtiShared/Result");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var AppDetailPanel = /** @class */ (function () {
    function AppDetailPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.app = new AppComponent_1.AppComponent(this.vm.app, this.hubApi);
        this.currentVersion = new CurrentVersionComponent_1.CurrentVersionComponent(this.vm.currentVersion, this.hubApi);
        this.resourceGroupListCard = new ResourceGroupListCard_1.ResourceGroupListCard(this.vm.resourceGroupListCard, this.hubApi);
        this.modifierCategoryListCard = new ModifierCategoryListCard_1.ModifierCategoryListCard(this.vm.modifierCategoryListCard, this.hubApi);
        this.mostRecentRequestListCard = new MostRecentRequestListCard_1.MostRecentRequestListCard(this.vm.mostRecentRequestListCard, this.hubApi);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(this.vm.mostRecentErrorEventListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.vm.backCommand, this.back.bind(this));
        var backIcon = this.backCommand.icon();
        backIcon.setName('fa-caret-left');
        this.backCommand.setText('Apps');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
        this.resourceGroupListCard.resourceGroupSelected.register(this.onResourceGroupSelected.bind(this));
        this.modifierCategoryListCard.modCategorySelected.register(this.onModCategorySelected.bind(this));
    }
    AppDetailPanel.prototype.onResourceGroupSelected = function (resourceGroup) {
        this.awaitable.resolve(new Result_1.Result(AppDetailPanel.ResultKeys.resourceGroupSelected, resourceGroup));
    };
    AppDetailPanel.prototype.onModCategorySelected = function (modCategory) {
        this.awaitable.resolve(new Result_1.Result(AppDetailPanel.ResultKeys.modCategorySelected, modCategory));
    };
    AppDetailPanel.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var promises;
            return tslib_1.__generator(this, function (_a) {
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
        this.awaitable.resolve(new Result_1.Result(AppDetailPanel.ResultKeys.backRequested));
    };
    AppDetailPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    AppDetailPanel.ResultKeys = {
        backRequested: 'back-requested',
        resourceGroupSelected: 'resource-group-selected',
        modCategorySelected: 'mod-category-selected'
    };
    return AppDetailPanel;
}());
exports.AppDetailPanel = AppDetailPanel;
//# sourceMappingURL=AppDetailPanel.js.map