"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleAccessListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var ContextualClass_1 = require("@jasonbenfield/sharedwebapp/ContextualClass");
var FaIcon_1 = require("@jasonbenfield/sharedwebapp/FaIcon");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var RoleAccessListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(RoleAccessListItemView, _super);
    function RoleAccessListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.icon = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new FaIcon_1.FaIcon());
        _this.icon.regularStyle();
        _this.icon.makeFixedWidth();
        _this.roleName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    RoleAccessListItemView.prototype.allowAccess = function () {
        this.updateIsAllowed(true);
    };
    RoleAccessListItemView.prototype.denyAccess = function () {
        this.updateIsAllowed(false);
    };
    RoleAccessListItemView.prototype.updateIsAllowed = function (isAllowed) {
        this.icon.setName(isAllowed ? 'thumbs-up' : 'thumbs-down');
        this.icon.addCssFrom(new TextCss_1.TextCss().context(isAllowed
            ? ContextualClass_1.ContextualClass.success
            : ContextualClass_1.ContextualClass.danger).cssClass());
    };
    return RoleAccessListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.RoleAccessListItemView = RoleAccessListItemView;
//# sourceMappingURL=RoleAccessListItemView.js.map