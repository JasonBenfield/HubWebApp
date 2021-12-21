"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var HubTheme_1 = require("../../HubTheme");
var UserComponentView_1 = require("./UserComponentView");
var UserModCategoryListCardView_1 = require("./UserModCategoryListCardView");
var UserRoleListCardView_1 = require("./UserRoleListCardView");
var AppUserPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppUserPanelView, _super);
    function AppUserPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.userComponent = flexFill.addContent(new UserComponentView_1.UserComponentView())
            .configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.userRoles = flexFill.addContent(new UserRoleListCardView_1.UserRoleListCardView())
            .configure(function (c) { return c.setMargin(MarginCss_1.MarginCss.bottom(3)); });
        _this.userModCategories = flexFill.addContent(new UserModCategoryListCardView_1.UserModCategoryListCardView());
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        _this.backButton.setText('User');
        return _this;
    }
    return AppUserPanelView;
}(Block_1.Block));
exports.AppUserPanelView = AppUserPanelView;
//# sourceMappingURL=AppUserPanelView.js.map