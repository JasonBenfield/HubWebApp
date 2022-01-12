"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var TextBlockView_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlockView");
var UnorderedList_1 = require("@jasonbenfield/sharedwebapp/Html/UnorderedList");
var ModCategoryComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModCategoryComponentView, _super);
    function ModCategoryComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        var listItem = _this.addCardBody()
            .addContent(new UnorderedList_1.UnorderedList())
            .addListItem();
        _this.modCategoryName = listItem.addContent(new TextBlockView_1.TextBlockView());
        return _this;
    }
    return ModCategoryComponentView;
}(CardView_1.CardView));
exports.ModCategoryComponentView = ModCategoryComponentView;
//# sourceMappingURL=ModCategoryComponentView.js.map