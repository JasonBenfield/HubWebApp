"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var ModCategoryComponentView = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryComponentView, _super);
    function ModCategoryComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert();
        _this.listGroup = _this.addBlockListGroup();
        var listItem = new ListGroupItemView_1.ListGroupItemView();
        _this.listGroup.addItem(listItem);
        _this.modCategoryName = listItem.addContent(new TextSpanView_1.TextSpanView());
        _this.clicked = _this.listGroup.itemClicked;
        return _this;
    }
    ModCategoryComponentView.prototype.showModCategory = function () { this.listGroup.show(); };
    ModCategoryComponentView.prototype.hideModCategory = function () { this.listGroup.hide(); };
    return ModCategoryComponentView;
}(CardView_1.CardView));
exports.ModCategoryComponentView = ModCategoryComponentView;
//# sourceMappingURL=ModCategoryComponentView.js.map