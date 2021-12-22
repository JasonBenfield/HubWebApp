"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ModifierCategoryListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModifierCategoryListItemView, _super);
    function ModifierCategoryListItemView() {
        var _this = _super.call(this) || this;
        _this.categoryName = _this.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpan_1.TextSpan());
        return _this;
    }
    ModifierCategoryListItemView.prototype.setCategoryName = function (name) { this.categoryName.setText(name); };
    return ModifierCategoryListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.ModifierCategoryListItemView = ModifierCategoryListItemView;
//# sourceMappingURL=ModifierCategoryListItemView.js.map