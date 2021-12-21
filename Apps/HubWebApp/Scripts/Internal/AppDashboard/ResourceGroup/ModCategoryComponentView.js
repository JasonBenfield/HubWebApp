"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ListItem_1 = require("@jasonbenfield/sharedwebapp/Html/ListItem");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ModCategoryComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModCategoryComponentView, _super);
    function ModCategoryComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        _this.listGroup = _this.addBlockListGroup();
        var listItem = new ListItem_1.ListItem();
        _this.listGroup.addItem(listItem);
        _this.modCategoryName = listItem.addContent(new TextSpan_1.TextSpan());
        _this.clicked = _this.listGroup.itemClicked;
        return _this;
    }
    ModCategoryComponentView.prototype.showModCategory = function () { this.listGroup.show(); };
    ModCategoryComponentView.prototype.hideModCategory = function () { this.listGroup.hide(); };
    ModCategoryComponentView.prototype.setModCategoryName = function (name) { this.modCategoryName.setText(name); };
    return ModCategoryComponentView;
}(CardView_1.CardView));
exports.ModCategoryComponentView = ModCategoryComponentView;
//# sourceMappingURL=ModCategoryComponentView.js.map