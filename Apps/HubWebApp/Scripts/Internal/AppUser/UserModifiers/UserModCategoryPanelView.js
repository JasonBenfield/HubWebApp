"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModCategoryPanelView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var DropDownFormGroupView_1 = require("@jasonbenfield/sharedwebapp/Forms/DropDownFormGroupView");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var HubTheme_1 = require("../../HubTheme");
var EditUserModifierListItemView_1 = require("./EditUserModifierListItemView");
var UserModCategoryPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserModCategoryPanelView, _super);
    function UserModCategoryPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var card = flexFill.addContent(new CardView_1.CardView());
        _this.titleHeader = card.addCardTitleHeader();
        _this.alert = card.addCardAlert().alert;
        var body = card.addCardBody();
        _this.hasAccessToAll = body.addContent(new DropDownFormGroupView_1.DropDownFormGroupView());
        _this.userModifiers = card.addBlockListGroup(function () { return new EditUserModifierListItemView_1.EditUserModifierListItemView(); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    UserModCategoryPanelView.prototype.showUserModifiers = function () { this.userModifiers.show(); };
    UserModCategoryPanelView.prototype.hideUserModifiers = function () { this.userModifiers.hide(); };
    return UserModCategoryPanelView;
}(Block_1.Block));
exports.UserModCategoryPanelView = UserModCategoryPanelView;
//# sourceMappingURL=UserModCategoryPanelView.js.map