"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourcePanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var MostRecentErrorEventListCardView_1 = require("../MostRecentErrorEventListCardView");
var MostRecentRequestListCardView_1 = require("../MostRecentRequestListCardView");
var ResourceAccessCardView_1 = require("../ResourceAccessCardView");
var ResourceComponentView_1 = require("./ResourceComponentView");
var ResourcePanelView = /** @class */ (function (_super) {
    tslib_1.__extends(ResourcePanelView, _super);
    function ResourcePanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.resourceComponent = flexFill
            .addContent(new ResourceComponentView_1.ResourceComponentView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.resourceAccessCard = flexFill
            .addContent(new ResourceAccessCardView_1.ResourceAccessCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentRequestListCard = flexFill
            .addContent(new MostRecentRequestListCardView_1.MostRecentRequestListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.mostRecentErrorEventListCard = flexFill
            .addContent(new MostRecentErrorEventListCardView_1.MostRecentErrorEventListCardView())
            .configure(function (b) { return b.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        _this.backButton.setText('Resource Group');
        return _this;
    }
    return ResourcePanelView;
}(Block_1.Block));
exports.ResourcePanelView = ResourcePanelView;
//# sourceMappingURL=ResourcePanelView.js.map