"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Result_1 = require("XtiShared/Result");
var Command_1 = require("XtiShared/Command/Command");
var ResourceGroupAccessCard_1 = require("./ResourceGroupAccessCard");
var ResourceGroupComponent_1 = require("./ResourceGroupComponent");
var ResourceListCard_1 = require("./ResourceListCard");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var MostRecentErrorEventListCard_1 = require("./MostRecentErrorEventListCard");
var ModCategoryComponent_1 = require("./ModCategoryComponent");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var MarginCss_1 = require("XtiShared/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var ResourceGroupPanel = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroupPanel, _super);
    function ResourceGroupPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.resourceGroupComponent = flexFill.addContent(new ResourceGroupComponent_1.ResourceGroupComponent(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modCategoryComponent = flexFill.addContent(new ModCategoryComponent_1.ModCategoryComponent(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modCategoryComponent.clicked.register(_this.onModCategoryClicked.bind(_this));
        _this.roleAccessCard = flexFill.addContent(new ResourceGroupAccessCard_1.ResourceGroupAccessCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceListCard = flexFill.addContent(new ResourceListCard_1.ResourceListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceListCard.resourceSelected.register(_this.onResourceSelected.bind(_this));
        _this.mostRecentRequestListCard = flexFill.addContent(new MostRecentRequestListCard_1.MostRecentRequestListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentErrorEventListCard = flexFill.addContent(new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(_this.hubApi)).configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton())).configure(function (b) { return b.setText('App'); });
        return _this;
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
        this.awaitable.resolve(new Result_1.Result(ResourceGroupPanel.ResultKeys.backRequested));
    };
    ResourceGroupPanel.ResultKeys = {
        backRequested: 'back-requested',
        resourceSelected: 'resource-selected',
        modCategorySelected: 'mod-category-selected'
    };
    return ResourceGroupPanel;
}(Block_1.Block));
exports.ResourceGroupPanel = ResourceGroupPanel;
//# sourceMappingURL=ResourceGroupPanel.js.map