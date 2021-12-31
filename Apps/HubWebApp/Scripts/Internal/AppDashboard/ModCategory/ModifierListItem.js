"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ModifierListItem = /** @class */ (function () {
    function ModifierListItem(modifier, view) {
        var modKey = new TextBlock_1.TextBlock(modifier.ModKey, view.modKey);
        modKey.syncTitleWithText();
        var displayText = new TextBlock_1.TextBlock(modifier.DisplayText, view.displayText);
        displayText.syncTitleWithText();
    }
    return ModifierListItem;
}());
exports.ModifierListItem = ModifierListItem;
//# sourceMappingURL=ModifierListItem.js.map