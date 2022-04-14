"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListItem = void 0;
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ResourceGroupListItem = /** @class */ (function () {
    function ResourceGroupListItem(group, view) {
        this.group = group;
        new TextBlock_1.TextBlock(group.Name, view.groupName);
    }
    return ResourceGroupListItem;
}());
exports.ResourceGroupListItem = ResourceGroupListItem;
//# sourceMappingURL=ResourceGroupListItem.js.map