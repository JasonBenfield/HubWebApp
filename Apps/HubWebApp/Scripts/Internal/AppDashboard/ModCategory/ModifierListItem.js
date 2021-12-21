"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModifierListItem = void 0;
var ModifierListItem = /** @class */ (function () {
    function ModifierListItem(modifier, view) {
        view.setModKey(modifier.ModKey);
        view.setDisplayText(modifier.DisplayText);
    }
    return ModifierListItem;
}());
exports.ModifierListItem = ModifierListItem;
//# sourceMappingURL=ModifierListItem.js.map