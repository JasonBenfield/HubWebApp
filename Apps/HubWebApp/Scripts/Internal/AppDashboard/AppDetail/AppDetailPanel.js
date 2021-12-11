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
var Command_1 = require("XtiShared/Command/Command");
var Result_1 = require("XtiShared/Result");
var MostRecentRequestListCard_1 = require("./MostRecentRequestListCard");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var MarginCss_1 = require("XtiShared/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var AppDetailPanel = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppDetailPanel, _super);
    function AppDetailPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.app = flexFill
            .addContent(new AppComponent_1.AppComponent(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.currentVersion = flexFill
            .addContent(new CurrentVersionComponent_1.CurrentVersionComponent(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCard_1.ResourceGroupListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceGroupListCard.resourceGroupSelected.register(_this.onResourceGroupSelected.bind(_this));
        _this.modifierCategoryListCard = flexFill
            .addContent(new ModifierCategoryListCard_1.ModifierCategoryListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modifierCategoryListCard.modCategorySelected.register(_this.onModCategorySelected.bind(_this));
        _this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCard_1.MostRecentRequestListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCard_1.MostRecentErrorEventListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton()));
        return _this;
    }
    AppDetailPanel.prototype.onResourceGroupSelected = function (resourceGroup) {
        this.awaitable.resolve(new Result_1.Result(AppDetailPanel.ResultKeys.resourceGroupSelected, resourceGroup));
    };
    AppDetailPanel.prototype.onModCategorySelected = function (modCategory) {
        this.awaitable.resolve(new Result_1.Result(AppDetailPanel.ResultKeys.modCategorySelected, modCategory));
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
}(Block_1.Block));
exports.AppDetailPanel = AppDetailPanel;
//# sourceMappingURL=AppDetailPanel.js.map