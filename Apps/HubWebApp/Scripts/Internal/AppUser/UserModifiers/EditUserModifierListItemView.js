"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserModifierListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var ContextualClass_1 = require("@jasonbenfield/sharedwebapp/ContextualClass");
var FaIcon_1 = require("@jasonbenfield/sharedwebapp/FaIcon");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var EditUserModifierListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EditUserModifierListItemView, _super);
    function EditUserModifierListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.icon = row.addColumn()
            .configure(function (col) { return col.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new FaIcon_1.FaIcon('square'))
            .configure(function (icon) {
            icon.makeFixedWidth();
            icon.regularStyle();
        });
        _this.modKey = row.addColumn()
            .addContent(new TextSpan_1.TextSpan(''));
        return _this;
    }
    EditUserModifierListItemView.prototype.setModKey = function (modKey) { this.modKey.setText(modKey); };
    EditUserModifierListItemView.prototype.setModDisplayText = function (displayText) { this.modDisplayText.setText(displayText); };
    EditUserModifierListItemView.prototype.startAssignment = function () {
        this.disable();
        this.icon.solidStyle();
        this.icon.setName('sync-alt');
        this.icon.startAnimation('spin');
    };
    EditUserModifierListItemView.prototype.assign = function () {
        this.icon.regularStyle();
        this.icon.setName('check-square');
        this.icon.setColor(ContextualClass_1.ContextualClass.success);
    };
    EditUserModifierListItemView.prototype.unassign = function () {
        this.icon.regularStyle();
        this.icon.setName('square');
        this.icon.setColor(ContextualClass_1.ContextualClass.default);
    };
    EditUserModifierListItemView.prototype.endAssignment = function () {
        this.enable();
        this.icon.stopAnimation();
    };
    return EditUserModifierListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.EditUserModifierListItemView = EditUserModifierListItemView;
//# sourceMappingURL=EditUserModifierListItemView.js.map