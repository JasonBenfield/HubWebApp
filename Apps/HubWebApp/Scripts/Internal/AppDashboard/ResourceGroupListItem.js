"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListItem = void 0;
var ResourceGroupListItem = /** @class */ (function () {
    function ResourceGroupListItem(group, view) {
        this.group = group;
        view.setGroupName(group.Name);
    }
    return ResourceGroupListItem;
}());
exports.ResourceGroupListItem = ResourceGroupListItem;
//# sourceMappingURL=ResourceGroupListItem.js.map