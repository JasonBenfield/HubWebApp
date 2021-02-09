"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourcePanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("XtiShared/Result");
var Command_1 = require("XtiShared/Command/Command");
var ResourceComponent_1 = require("./ResourceComponent");
var ResourceAccessCard_1 = require("./ResourceAccessCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var MarginCss_1 = require("XtiShared/MarginCss");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var HubTheme_1 = require("../../HubTheme");
var ResourcePanel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourcePanel, _super);
    function ResourcePanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.resourceComponent = flexFill
            .addContent(new ResourceComponent_1.ResourceComponent(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceAccessCard = flexFill
            .addContent(new ResourceAccessCard_1.ResourceAccessCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCard_1.MostRecentRequestListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        var backButton = _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton()));
        backButton.setText('Resource Group');
        return _this;
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
}(Block_1.Block));
exports.ResourcePanel = ResourcePanel;
//# sourceMappingURL=ResourcePanel.js.map