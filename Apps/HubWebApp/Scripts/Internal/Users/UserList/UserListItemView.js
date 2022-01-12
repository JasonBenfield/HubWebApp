"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var UserListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserListItemView, _super);
    function UserListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.userName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs(4)); })
            .addContent(new TextSpanView_1.TextSpanView());
        _this.fullName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return UserListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.UserListItemView = UserListItemView;
//# sourceMappingURL=UserListItemView.js.map