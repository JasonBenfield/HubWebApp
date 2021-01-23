"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourcePanel = void 0;
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("XtiShared/Result");
var Command_1 = require("XtiShared/Command");
var ResourceComponent_1 = require("./ResourceComponent");
var ResourceAccessCard_1 = require("./ResourceAccessCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var ResourcePanel = /** @class */ (function () {
    function ResourcePanel(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.resourceComponent = new ResourceComponent_1.ResourceComponent(this.vm.resourceComponent, this.hubApi);
        this.resourceAccessCard = new ResourceAccessCard_1.ResourceAccessCard(this.vm.resourceAccessCard, this.hubApi);
        this.mostRecentRequestListCard = new MostRecentRequestListCard_1.MostRecentRequestListCard(this.vm.mostRecentRequestListCard, this.hubApi);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(this.vm.mostRecentErrorEventListCard, this.hubApi);
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.vm.backCommand, this.back.bind(this));
        var icon = this.backCommand.icon();
        icon.setName('fa-caret-left');
        this.backCommand.setText('Resource Group');
        this.backCommand.setTitle('Back');
        this.backCommand.makeLight();
    }
    ResourcePanel.prototype.setResourceID = function (resourceID) {
        this.resourceComponent.setResourceID(resourceID);
        this.resourceAccessCard.setResourceID(resourceID);
        this.mostRecentRequestListCard.setResourceID(resourceID);
        this.mostRecentErrorEventListCard.setResourceID(resourceID);
    };
    ResourcePanel.prototype.refresh = function () {
        var promises = [
            this.resourceComponent.refresh(),
            this.resourceAccessCard.refresh(),
            this.mostRecentRequestListCard.refresh(),
            this.mostRecentErrorEventListCard.refresh()
        ];
        return Promise.all(promises);
    };
    ResourcePanel.prototype.start = function () {
        return this.awaitable.start();
    };
    ResourcePanel.prototype.back = function () {
        this.awaitable.resolve(new Result_1.Result(ResourcePanel.ResultKeys.backRequested));
    };
    ResourcePanel.ResultKeys = {
        backRequested: 'back-requested'
    };
    return ResourcePanel;
}());
exports.ResourcePanel = ResourcePanel;
//# sourceMappingURL=ResourcePanel.js.map