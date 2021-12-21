"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ResourceGroupListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroupListItemView, _super);
    function ResourceGroupListItemView() {
        var _this = _super.call(this) || this;
        var row = new Row_1.Row();
        _this.groupName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        return _this;
    }
    ResourceGroupListItemView.prototype.setGroupName = function (groupName) { this.groupName.setText(groupName); };
    return ResourceGroupListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.ResourceGroupListItemView = ResourceGroupListItemView;
//# sourceMappingURL=ResourceGroupListItemView.js.map