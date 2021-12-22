"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryButtonListItemView = void 0;
var tslib_1 = require("tslib");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ModCategoryButtonListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ModCategoryButtonListItemView, _super);
    function ModCategoryButtonListItemView() {
        var _this = _super.call(this) || this;
        _this.categoryName = _this.addContent(new TextBlock_1.TextBlock);
        return _this;
    }
    ModCategoryButtonListItemView.prototype.setCategoryName = function (name) { this.categoryName.setText(name); };
    return ModCategoryButtonListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.ModCategoryButtonListItemView = ModCategoryButtonListItemView;
//# sourceMappingURL=ModCategoryButtonListItemView.js.map