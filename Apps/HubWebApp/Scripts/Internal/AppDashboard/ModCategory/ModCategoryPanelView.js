"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var ModCategoryComponentView_1 = require("./ModCategoryComponentView");
var ModifierListCardView_1 = require("./ModifierListCardView");
var ResourceGroupListCardView_1 = require("./ResourceGroupListCardView");
var ModCategoryPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModCategoryPanelView, _super);
    function ModCategoryPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.modCategoryComponent = flexFill
            .addContent(new ModCategoryComponentView_1.ModCategoryComponentView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modifierListCard = flexFill
            .addContent(new ModifierListCardView_1.ModifierListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceGroupListCard = flexFill
            .addContent(new ResourceGroupListCardView_1.ResourceGroupListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        _this.backButton.setText('App');
        return _this;
    }
    return ModCategoryPanelView;
}(Block_1.Block));
exports.ModCategoryPanelView = ModCategoryPanelView;
//# sourceMappingURL=ModCategoryPanelView.js.map