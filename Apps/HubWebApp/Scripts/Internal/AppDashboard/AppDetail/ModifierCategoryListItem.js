"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierCategoryListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ModifierCategoryListItem = /** @class */ (function () {
    function ModifierCategoryListItem(modCategory, view) {
        this.modCategory = modCategory;
        new TextBlock_1.TextBlock(modCategory.Name, view.categoryName);
    }
    return ModifierCategoryListItem;
}());
exports.ModifierCategoryListItem = ModifierCategoryListItem;
//# sourceMappingURL=ModifierCategoryListItem.js.map