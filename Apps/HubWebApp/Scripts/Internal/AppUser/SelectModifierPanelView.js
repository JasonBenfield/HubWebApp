"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectModifierPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var ListBlockViewModel_1 = require("@jasonbenfield/sharedwebapp/Html/ListBlockViewModel");
var ListGroupView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupView");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var MessageAlertView_1 = require("@jasonbenfield/sharedwebapp/MessageAlertView");
var HubTheme_1 = require("../HubTheme");
var ModifierButtonListItemView_1 = require("./ModifierButtonListItemView");
var SelectModifierPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(SelectModifierPanelView, _super);
    function SelectModifierPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.alert = flexFill.addContent(new MessageAlertView_1.MessageAlertView());
        _this.modifiers = _this.addContent(new ListGroupView_1.ListGroupView(function () { return new ModifierButtonListItemView_1.ModifierButtonListItemView(); }, new ListBlockViewModel_1.ListBlockViewModel()));
        _this.modifiers.setMargin(MarginCss_1.MarginCss.bottom(3));
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    return SelectModifierPanelView;
}(Block_1.Block));
exports.SelectModifierPanelView = SelectModifierPanelView;
//# sourceMappingURL=SelectModifierPanelView.js.map