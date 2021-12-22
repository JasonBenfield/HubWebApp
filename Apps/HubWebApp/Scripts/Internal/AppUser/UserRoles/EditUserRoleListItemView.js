"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserRoleListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var ContextualClass_1 = require("@jasonbenfield/sharedwebapp/ContextualClass");
var FaIcon_1 = require("@jasonbenfield/sharedwebapp/FaIcon");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var EditUserRoleListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EditUserRoleListItemView, _super);
    function EditUserRoleListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.icon = row.addColumn()
            .configure(function (col) { return col.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new FaIcon_1.FaIcon('square'))
            .configure(function (icon) {
            icon.makeFixedWidth();
            icon.regularStyle();
        });
        _this.roleName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan(''));
        return _this;
    }
    EditUserRoleListItemView.prototype.setRoleName = function (roleName) { this.roleName.setText(roleName); };
    EditUserRoleListItemView.prototype.startAssignment = function () {
        this.disable();
        this.icon.solidStyle();
        this.icon.setName('sync-alt');
        this.icon.startAnimation('spin');
    };
    EditUserRoleListItemView.prototype.assign = function () {
        this.icon.regularStyle();
        this.icon.setName('check-square');
        this.icon.setColor(ContextualClass_1.ContextualClass.success);
    };
    EditUserRoleListItemView.prototype.unassign = function () {
        this.icon.regularStyle();
        this.icon.setName('square');
        this.icon.setColor(ContextualClass_1.ContextualClass.default);
    };
    EditUserRoleListItemView.prototype.endAssignment = function () {
        this.enable();
        this.icon.stopAnimation();
    };
    return EditUserRoleListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.EditUserRoleListItemView = EditUserRoleListItemView;
//# sourceMappingURL=EditUserRoleListItemView.js.map