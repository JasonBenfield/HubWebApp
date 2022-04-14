"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRolesPanelView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var ButtonCommandItem_1 = require("@jasonbenfield/sharedwebapp/Command/ButtonCommandItem");
var FlexCss_1 = require("@jasonbenfield/sharedwebapp/FlexCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var Heading1_1 = require("@jasonbenfield/sharedwebapp/Html/Heading1");
var Heading3_1 = require("@jasonbenfield/sharedwebapp/Html/Heading3");
var NavView_1 = require("@jasonbenfield/sharedwebapp/Html/NavView");
var TextSmallView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSmallView");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var HubTheme_1 = require("../HubTheme");
var UserRoleListItemView_1 = require("./UserRoleListItemView");
var UserRolesPanelView = /** @class */ (function (_super) {
    tslib_1.__extends(UserRolesPanelView, _super);
    function UserRolesPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var userHeading = flexFill.addContent(new Heading1_1.Heading1());
        _this.personName = userHeading.addContent(new TextSpanView_1.TextSpanView());
        _this.userName = userHeading.addContent(new TextSmallView_1.TextSmallView());
        userHeading.setMargin(MarginCss_1.MarginCss.bottom(3));
        var appHeading = flexFill.addContent(new Heading3_1.Heading3());
        _this.appName = appHeading.addContent(new TextSpanView_1.TextSpanView());
        _this.appType = appHeading.addContent(new TextSmallView_1.TextSmallView());
        appHeading.setMargin(MarginCss_1.MarginCss.bottom(3));
        var card = flexFill.addContent(new CardView_1.CardView());
        var categoryHeader = card.addCardTitleHeader();
        var categoryRow = categoryHeader.addContent(new Row_1.Row());
        var modCol = categoryRow.addColumn()
            .configure(function (c) { return c.setPadding(PaddingCss_1.PaddingCss.top(1)); });
        _this.categoryName = modCol.addContent(new TextSpanView_1.TextSpanView());
        _this.modifierDisplayText = modCol.addContent(new TextSpanView_1.TextSpanView());
        modCol.addContent(new TextSpanView_1.TextSpanView())
            .configure(function (ts) { return ts.setText('Roles'); });
        _this.addButton = categoryRow
            .addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(HubTheme_1.HubTheme.instance.cardHeader.addButton());
        _this.addButton.setTitle('Add Role');
        var body = card.addCardBody();
        var navPills = body.addContent(new NavView_1.NavView());
        navPills.pills();
        navPills.setFlexCss(new FlexCss_1.FlexCss().column().fill());
        _this.selectModifierButton = _this.addNavLinkButton(navPills);
        _this.selectModifierButton.icon.setName('pencil-alt');
        _this.selectModifierButton.icon.solidStyle();
        _this.selectModifierButton.setText('Select Different Modifier');
        _this.allowAccessButton = _this.addNavLinkButton(navPills);
        _this.allowAccessButton.icon.setName('check');
        _this.allowAccessButton.icon.solidStyle();
        _this.allowAccessButton.setText('Allow Access');
        _this.denyAccessButton = _this.addNavLinkButton(navPills);
        _this.denyAccessButton.icon.setName('ban');
        _this.denyAccessButton.icon.solidStyle();
        _this.denyAccessButton.setText('Deny Access');
        _this.alert = card.addCardAlert();
        _this.userRoles = card.addBlockListGroup(function () { return new UserRoleListItemView_1.UserRoleListItemView(); });
        card.setMargin(MarginCss_1.MarginCss.bottom(3));
        _this.defaultUserRolesCard = flexFill.addContent(new CardView_1.CardView());
        _this.defaultUserRolesTitle = _this.defaultUserRolesCard.addCardTitleHeader();
        _this.defaultUserRoles = _this.defaultUserRolesCard.addUnorderedListGroup(function () { return new UserRoleListItemView_1.UserRoleListItemView(); });
        return _this;
    }
    UserRolesPanelView.prototype.addNavLinkButton = function (navPills) {
        var navLinkButton = navPills.addContent(new ButtonCommandItem_1.ButtonCommandItem());
        navLinkButton.addCssName('nav-link');
        navLinkButton.setTextCss(new TextCss_1.TextCss().start());
        return navLinkButton;
    };
    UserRolesPanelView.prototype.showDefaultUserRoles = function () {
        this.defaultUserRolesCard.show();
    };
    UserRolesPanelView.prototype.hideDefaultUserRoles = function () {
        this.defaultUserRolesCard.hide();
    };
    return UserRolesPanelView;
}(Block_1.Block));
exports.UserRolesPanelView = UserRolesPanelView;
//# sourceMappingURL=UserRolesPanelView.js.map