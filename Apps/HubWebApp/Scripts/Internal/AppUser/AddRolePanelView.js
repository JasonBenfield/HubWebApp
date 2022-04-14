"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AddRolePanelView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var HubTheme_1 = require("../HubTheme");
var RoleButtonListItemView_1 = require("./RoleButtonListItemView");
var AddRolePanelView = /** @class */ (function (_super) {
    tslib_1.__extends(AddRolePanelView, _super);
    function AddRolePanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var card = flexFill.addContent(new CardView_1.CardView());
        _this.titleHeader = card.addCardTitleHeader();
        _this.alert = card.addCardAlert();
        _this.roles = card.addBlockListGroup(function () { return new RoleButtonListItemView_1.RoleButtonListItemView(); });
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    return AddRolePanelView;
}(Block_1.Block));
exports.AddRolePanelView = AddRolePanelView;
//# sourceMappingURL=AddRolePanelView.js.map