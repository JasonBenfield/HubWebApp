"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleAccessListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var RoleAccessListItemView = /** @class */ (function (_super) {
    tslib_1.__extends(RoleAccessListItemView, _super);
    function RoleAccessListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.roleName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return RoleAccessListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.RoleAccessListItemView = RoleAccessListItemView;
//# sourceMappingURL=RoleAccessListItemView.js.map