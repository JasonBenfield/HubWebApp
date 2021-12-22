"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectModCategoryPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var ListBlockViewModel_1 = require("@jasonbenfield/sharedwebapp/Html/ListBlockViewModel");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var UnorderedList_1 = require("@jasonbenfield/sharedwebapp/Html/UnorderedList");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ListGroupView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupView");
var MarginCss_1 = require("@jasonbenfield/sharedwebapp/MarginCss");
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var HubTheme_1 = require("../HubTheme");
var ModCategoryButtonListItemView_1 = require("./ModCategoryButtonListItemView");
var SelectModCategoryPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(SelectModCategoryPanelView, _super);
    function SelectModCategoryPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        flexFill.setPadding(PaddingCss_1.PaddingCss.top(3));
        _this.defaultModListItem = new ButtonListGroupItemView_1.ButtonListGroupItemView();
        _this.defaultModClicked = _this.defaultModListItem.clicked;
        _this.defaultModListItem.addContent(new TextBlock_1.TextBlock('Default Modifier'));
        var defaultModList = flexFill.addContent(new UnorderedList_1.UnorderedList());
        defaultModList.addItem(_this.defaultModListItem);
        defaultModList.setMargin(MarginCss_1.MarginCss.bottom(3));
        _this.modCategories = _this.addContent(new ListGroupView_1.ListGroupView(function () { return new ModCategoryButtonListItemView_1.ModCategoryButtonListItemView(); }, new ListBlockViewModel_1.ListBlockViewModel()));
        _this.modCategories.setMargin(MarginCss_1.MarginCss.bottom(3));
        var toolbar = flexColumn.addContent(HubTheme_1.HubTheme.instance.commandToolbar.toolbar());
        _this.backButton = toolbar.columnStart.addContent(HubTheme_1.HubTheme.instance.commandToolbar.backButton());
        return _this;
    }
    return SelectModCategoryPanelView;
}(Block_1.Block));
exports.SelectModCategoryPanelView = SelectModCategoryPanelView;
//# sourceMappingURL=SelectModCategoryPanelView.js.map