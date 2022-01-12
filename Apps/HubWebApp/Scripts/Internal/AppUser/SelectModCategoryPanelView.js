"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectModCategoryPanelView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var ListBlockViewModel_1 = require("@jasonbenfield/sharedwebapp/Html/ListBlockViewModel");
var ListItem_1 = require("@jasonbenfield/sharedwebapp/Html/ListItem");
var TextBlockView_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlockView");
var ListGroupView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupView");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var HubTheme_1 = require("../HubTheme");
var ModCategoryButtonListItemView_1 = require("./ModCategoryButtonListItemView");
var SelectModCategoryPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(SelectModCategoryPanelView, _super);
    function SelectModCategoryPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        _this.setName(SelectModCategoryPanelView.name);
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        var defaultModList = flexFill.addContent(new ListGroupView_1.ListGroupView(function () { return new ListItem_1.ListItem(); }, new ListBlockViewModel_1.ListBlockViewModel()));
        defaultModList.setMargin(MarginCss_1.MarginCss.bottom(3));
        _this.defaultModClicked = defaultModList.itemClicked;
        _this.defaultModListItem = defaultModList.addButtonListGroupItem();
        _this.defaultModListItem
            .addContent(new TextBlockView_1.TextBlockView())
            .configure(function (tb) { return tb.setText('Default Modifier'); });
        var card = flexFill.addContent(new CardView_1.CardView());
        _this.titleHeader = card.addCardTitleHeader();
        _this.alert = card.addCardAlert();
        _this.modCategories = card.addBlockListGroup(function () { return new ModCategoryButtonListItemView_1.ModCategoryButtonListItemView(); });
        card.setMargin(MarginCss_1.MarginCss.bottom(3));
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    return SelectModCategoryPanelView;
}(Block_1.Block));
exports.SelectModCategoryPanelView = SelectModCategoryPanelView;
//# sourceMappingURL=SelectModCategoryPanelView.js.map