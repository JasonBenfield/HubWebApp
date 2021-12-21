"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRolePanelView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var HubTheme_1 = require("../../HubTheme");
var EditUserRoleListItemView_1 = require("./EditUserRoleListItemView");
var UserRolePanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserRolePanelView, _super);
    function UserRolePanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var card = flexFill.addContent(new CardView_1.CardView());
        _this.titleHeader = card.addCardTitleHeader();
        _this.alert = card.addCardAlert().alert;
        _this.userRoles = card.addBlockListGroup(function () { return new EditUserRoleListItemView_1.EditUserRoleListItemView(); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    return UserRolePanelView;
}(Block_1.Block));
exports.UserRolePanelView = UserRolePanelView;
//# sourceMappingURL=UserRolePanelView.js.map