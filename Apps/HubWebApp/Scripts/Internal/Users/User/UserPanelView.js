"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var AppListCardView_1 = require("../../Apps/AppListCardView");
var HubTheme_1 = require("../../HubTheme");
var UserComponentView_1 = require("./UserComponentView");
var UserPanelView = /** @class */ (function (_super) {
    tslib_1.__extends(UserPanelView, _super);
    function UserPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        _this.setName(UserPanelView.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.userComponent = flexFill.container.addContent(new UserComponentView_1.UserComponentView())
            .configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.appListCard = flexFill.container.addContent(new AppListCardView_1.AppListCardView());
        _this.appListCard.setMargin(MarginCss_1.MarginCss.bottom(3));
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        _this.backButton.setText('App Permissions');
        return _this;
    }
    return UserPanelView;
}(Block_1.Block));
exports.UserPanelView = UserPanelView;
//# sourceMappingURL=UserPanelView.js.map