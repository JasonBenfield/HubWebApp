"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var RoleListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(RoleListItemView, _super);
    function RoleListItemView() {
        var _this = _super.call(this) || this;
        _this.roleName = _this.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpan_1.TextSpan());
        return _this;
    }
    RoleListItemView.prototype.setRoleName = function (roleName) { this.roleName.setText(roleName); };
    return RoleListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.RoleListItemView = RoleListItemView;
//# sourceMappingURL=RoleListItemView.js.map