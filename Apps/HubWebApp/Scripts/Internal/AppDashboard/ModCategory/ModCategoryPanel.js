"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryPanel = void 0;
var tslib_1 = require("tslib");
var Awaitable_1 = require("XtiShared/Awaitable");
var Command_1 = require("XtiShared/Command/Command");
var Result_1 = require("XtiShared/Result");
var Block_1 = require("XtiShared/Html/Block");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var FlexColumn_1 = require("XtiShared/Html/FlexColumn");
var FlexColumnFill_1 = require("XtiShared/Html/FlexColumnFill");
var MarginCss_1 = require("XtiShared/MarginCss");
var ModCategoryComponent_1 = require("./ModCategoryComponent");
var ModifierListCard_1 = require("./ModifierListCard");
var ResourceGroupListCard_1 = require("./ResourceGroupListCard");
var HubTheme_1 = require("../../HubTheme");
var ModCategoryPanel = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModCategoryPanel, _super);
    function ModCategoryPanel(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.awaitable = new Awaitable_1.Awaitable();
        _this.backCommand = new Command_1.Command(_this.back.bind(_this));
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.modCategoryComponent = flexFill
            .addContent(new ModCategoryComponent_1.ModCategoryComponent(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modifierListCard = flexFill
            .addContent(new ModifierListCard_1.ModifierListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCard_1.ResourceGroupListCard(_this.hubApi))
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backCommand.add(toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton())).configure(function (b) { return b.setText('App'); });
        _this.resourceGroupListCard.resourceGroupSelected.register(_this.onResourceGroupSelected.bind(_this));
        return _this;
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
}(Block_1.Block));
exports.ModCategoryPanel = ModCategoryPanel;
//# sourceMappingURL=ModCategoryPanel.js.map