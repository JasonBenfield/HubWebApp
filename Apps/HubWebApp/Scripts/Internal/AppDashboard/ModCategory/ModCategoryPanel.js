"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryPanel = void 0;
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command");
var Result_1 = require("XtiShared/Result");
var ModCategoryComponent_1 = require("./ModCategoryComponent");
var ModifierListCard_1 = require("./ModifierListCard");
var ResourceGroupListCard_1 = require("./ResourceGroupListCard");
var ModCategoryPanel = /** @class */ (function () {
    function ModCategoryPanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.modCategoryComponent = new ModCategoryComponent_1.ModCategoryComponent(this.vm.modCategoryComponent, this.hubApi);
        this.modifierListCard = new ModifierListCard_1.ModifierListCard(this.vm.modifierListCard, this.hubApi);
        this.resourceGroupListCard = new ResourceGroupListCard_1.ResourceGroupListCard(this.vm.resourceGroupListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.vm.backCommand, this.back.bind(this));
        var backIcon = this.backCommand.icon();
        backIcon.setName('fa-caret-left');
        this.backCommand.setText('App');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
        this.resourceGroupListCard.resourceGroupSelected.register(this.onResourceGroupSelected.bind(this));
    }
    ModCategoryPanel.prototype.onResourceGroupSelected = function (resourceGroup) {
        this.awaitable.resolve(new Result_1.Result(ModCategoryPanel.ResultKeys.resourceGroupSelected, resourceGroup));
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
        this.awaitable.resolve(new Result_1.Result(ModCategoryPanel.ResultKeys.backRequested));
    };
    ModCategoryPanel.ResultKeys = {
        backRequested: 'back-requested',
        resourceGroupSelected: 'resource-group-selected'
    };
    return ModCategoryPanel;
}());
exports.ModCategoryPanel = ModCategoryPanel;
//# sourceMappingURL=ModCategoryPanel.js.map