"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModCategoryListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ModCategoryListItem = /** @class */ (function () {
    function ModCategoryListItem(modCategory, view) {
        this.modCategory = modCategory;
        new TextBlock_1.TextBlock(modCategory.Name, view.categoryName);
    }
    return ModCategoryListItem;
}());
exports.ModCategoryListItem = ModCategoryListItem;
//# sourceMappingURL=ModCategoryListItem.js.map