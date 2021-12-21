"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserEditPanelView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MessageAlertView_1 = require("@jasonbenfield/sharedwebapp/MessageAlertView");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var EditUserFormView_1 = require("../../../Hub/Api/EditUserFormView");
var HubTheme_1 = require("../../HubTheme");
var UserEditPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserEditPanelView, _super);
    function UserEditPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        _this.setName(UserEditPanelView.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.alert = flexFill.container.addContent(new MessageAlertView_1.MessageAlertView());
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.cancelButton = toolbar.columnEnd.addContent(HubTheme_1.HubTheme.instance.commandToolbar.cancelButton());
        _this.saveButton = toolbar.columnEnd.addContent(HubTheme_1.HubTheme.instance.commandToolbar.saveButton());
        var editCard = flexFill.addContent(new CardView_1.CardView());
        _this.titleHeader = editCard.addCardTitleHeader();
        var cardBody = editCard.addCardBody();
        _this.editUserForm = cardBody.addContent(new EditUserFormView_1.EditUserFormView());
        _this.editUserForm.addOffscreenSubmit();
        _this.editUserForm.executeLayout();
        _this.editUserForm.forEachFormGroup(function (fg) {
            fg.captionColumn.setTextCss(new TextCss_1.TextCss().end().bold());
        });
        return _this;
    }
    return UserEditPanelView;
}(Block_1.Block));
exports.UserEditPanelView = UserEditPanelView;
//# sourceMappingURL=UserEditPanelView.js.map