"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRoleListCardView = void 0;
var tslib_1 = require("tslib");
var AlignCss_1 = require("@jasonbenfield/sharedwebapp/AlignCss");
var CardHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardHeader");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var HubTheme_1 = require("../../HubTheme");
var RoleListItemView_1 = require("./RoleListItemView");
var UserRoleListCardView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserRoleListCardView, _super);
    function UserRoleListCardView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new CardHeader_1.CardHeader())
            .addContent(new Row_1.Row())
            .configure(function (r) { return r.addCssFrom(new AlignCss_1.AlignCss().items(function (a) { return a.xs('baseline'); }).cssClass()); });
        row.addColumn()
            .addContent(new TextSpan_1.TextSpan('User Roles'));
        _this.editButton = row.addColumn()
            .configure(function (col) { return col.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(HubTheme_1.HubTheme.instance.cardHeader.editButton());
        _this.alert = _this.addCardAlert().alert;
        _this.roles = _this.addUnorderedListGroup(function () { return new RoleListItemView_1.RoleListItemView(); });
        return _this;
    }
    return UserRoleListCardView;
}(CardView_1.CardView));
exports.UserRoleListCardView = UserRoleListCardView;
//# sourceMappingURL=UserRoleListCardView.js.map