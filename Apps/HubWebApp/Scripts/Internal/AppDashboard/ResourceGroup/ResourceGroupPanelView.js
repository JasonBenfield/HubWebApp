"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var MostRecentErrorEventListCardView_1 = require("../MostRecentErrorEventListCardView");
var MostRecentRequestListCardView_1 = require("../MostRecentRequestListCardView");
var ResourceAccessCardView_1 = require("../ResourceAccessCardView");
var ModCategoryComponentView_1 = require("./ModCategoryComponentView");
var ResourceGroupComponentView_1 = require("./ResourceGroupComponentView");
var ResourceListCardView_1 = require("./ResourceListCardView");
var ResourceGroupPanelView = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupPanelView, _super);
    function ResourceGroupPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.resourceGroupComponent = flexFill.addContent(new ResourceGroupComponentView_1.ResourceGroupComponentView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.modCategoryComponent = flexFill.addContent(new ModCategoryComponentView_1.ModCategoryComponentView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.roleAccessCard = flexFill.addContent(new ResourceAccessCardView_1.ResourceAccessCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceListCard = flexFill.addContent(new ResourceListCardView_1.ResourceListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentRequestListCard = flexFill.addContent(new MostRecentRequestListCardView_1.MostRecentRequestListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentErrorEventListCard = flexFill.addContent(new MostRecentErrorEventListCardView_1.MostRecentErrorEventListCardView()).configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        _this.backButton.setText('App');
        return _this;
    }
    return ResourceGroupPanelView;
}(Block_1.Block));
exports.ResourceGroupPanelView = ResourceGroupPanelView;
//# sourceMappingURL=ResourceGroupPanelView.js.map