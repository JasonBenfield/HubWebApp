"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppDetailPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var BlockViewModel_1 = require("@jasonbenfield/sharedwebapp/Html/BlockViewModel");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var AppComponentView_1 = require("./AppComponentView");
var CurrentVersionComponentView_1 = require("./CurrentVersionComponentView");
var ModifierCategoryListCardView_1 = require("./ModifierCategoryListCardView");
var MostRecentErrorEventListCardView_1 = require("./MostRecentErrorEventListCardView");
var MostRecentRequestListCardView_1 = require("./MostRecentRequestListCardView");
var ResourceGroupListCardView_1 = require("./ResourceGroupListCardView");
var AppDetailPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppDetailPanelView, _super);
    function AppDetailPanelView(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.app = flexFill
            .addContent(new AppComponentView_1.AppComponentView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.currentVersion = flexFill
            .addContent(new CurrentVersionComponentView_1.CurrentVersionComponentView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCardView_1.ResourceGroupListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modifierCategoryListCard = flexFill
            .addContent(new ModifierCategoryListCardView_1.ModifierCategoryListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCardView_1.MostRecentRequestListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCardView_1.MostRecentErrorEventListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    return AppDetailPanelView;
}(Block_1.Block));
exports.AppDetailPanelView = AppDetailPanelView;
//# sourceMappingURL=AppDetailPanelView.js.map