"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryButtonListItemView = void 0;
var tslib_1 = require("tslib");
var TextBlockView_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlockView");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ModCategoryButtonListItemView = /** @class */ (function (_super) {
    tslib_1.__extends(ModCategoryButtonListItemView, _super);
    function ModCategoryButtonListItemView() {
        var _this = _super.call(this) || this;
        _this.categoryName = _this.addContent(new TextBlockView_1.TextBlockView);
        return _this;
    }
    return ModCategoryButtonListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.ModCategoryButtonListItemView = ModCategoryButtonListItemView;
//# sourceMappingURL=ModCategoryButtonListItemView.js.map