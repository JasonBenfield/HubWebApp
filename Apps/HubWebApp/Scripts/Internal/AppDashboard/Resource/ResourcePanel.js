"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourcePanel = exports.ResourcePanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var Command_1 = require("@jasonbenfield/sharedwebapp/Command/Command");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var ResourceAccessCard_1 = require("./ResourceAccessCard");
var ResourceComponent_1 = require("./ResourceComponent");
var ResourcePanelResult = /** @class */ (function () {
    function ResourcePanelResult(results) {
        this.results = results;
    }
    Object.defineProperty(ResourcePanelResult, "backRequested", {
        get: function () { return new ResourcePanelResult({ backRequested: {} }); },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ResourcePanelResult.prototype, "backRequested", {
        get: function () { return this.results.backRequested; },
        enumerable: false,
        configurable: true
    });
    return ResourcePanelResult;
}());
exports.ResourcePanelResult = ResourcePanelResult;
var ResourcePanel = /** @class */ (function () {
    function ResourcePanel(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this.awaitable = new Awaitable_1.Awaitable();
        this.backCommand = new Command_1.Command(this.back.bind(this));
        this.resourceComponent = new ResourceComponent_1.ResourceComponent(this.hubApi, this.view.resourceComponent);
        this.resourceAccessCard = new ResourceAccessCard_1.ResourceAccessCard(this.hubApi, this.view.resourceAccessCard);
        this.mostRecentRequestListCard = new MostRecentRequestListCard_1.MostRecentRequestListCard(this.hubApi, this.view.mostRecentRequestListCard);
        this.mostRecentErrorEventListCard = new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(this.hubApi, this.view.mostRecentErrorEventListCard);
        this.backCommand.add(this.view.backButton);
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
        this.awaitable.resolve(ResourcePanelResult.backRequested);
    };
    ResourcePanel.prototype.activate = function () { this.view.show(); };
    ResourcePanel.prototype.deactivate = function () { this.view.hide(); };
    return ResourcePanel;
}());
exports.ResourcePanel = ResourcePanel;
//# sourceMappingURL=ResourcePanel.js.map