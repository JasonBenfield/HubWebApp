"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRoleListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextBlockView_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlockView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var HubTheme_1 = require("../HubTheme");
var UserRoleListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserRoleListItemView, _super);
    function UserRoleListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        var col1 = row.addColumn();
        var roleName = col1.addContent(new TextBlockView_1.TextBlockView());
        roleName.setPadding(PaddingCss_1.PaddingCss.top(1));
        _this.roleName = roleName;
        var col2 = row.addColumn();
        col2.setColumnCss(ColumnCss_1.ColumnCss.xs('auto'));
        _this.deleteButton = col2.addContent(HubTheme_1.HubTheme.instance.listItem.deleteButton());
        return _this;
    }
    return UserRoleListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.UserRoleListItemView = UserRoleListItemView;
//# sourceMappingURL=UserRoleListItemView.js.map