"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserModifierListItem = void 0;
var tslib_1 = require("tslib");
var FaIcon_1 = require("XtiShared/FaIcon");
var Row_1 = require("XtiShared/Grid/Row");
var ButtonListItemViewModel_1 = require("XtiShared/ListGroup/ButtonListItemViewModel");
var ButtonListGroupItem_1 = require("XtiShared/ListGroup/ButtonListGroupItem");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var ContextualClass_1 = require("XtiShared/ContextualClass");
var EditUserModifierListItem = /** @class */ (function (_super) {
    tslib_1.__extends(EditUserModifierListItem, _super);
    function EditUserModifierListItem(hubApi, vm) {
        if (vm === void 0) { vm = new ButtonListItemViewModel_1.ButtonListItemViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
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
        _this.clicked.register(_this.onClick.bind(_this));
        return _this;
    }
    EditUserModifierListItem.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    EditUserModifierListItem.prototype.onClick = function () {
        return this.toggleAssignment();
    };
    EditUserModifierListItem.prototype.toggleAssignment = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var _a;
            return tslib_1.__generator(this, function (_b) {
                switch (_b.label) {
                    case 0:
                        this.disable();
                        this.icon.solidStyle();
                        this.icon.setName('sync-alt');
                        this.icon.startAnimation('spin');
                        _b.label = 1;
                    case 1:
                        _b.trys.push([1, , 6, 7]);
                        if (!this.userRoleID) return [3 /*break*/, 3];
                        return [4 /*yield*/, this.hubApi.AppUserMaintenance.UnassignRole(this.userRoleID)];
                    case 2:
                        _b.sent();
                        this.unassignedIcon();
                        return [3 /*break*/, 5];
                    case 3:
                        _a = this;
                        return [4 /*yield*/, this.hubApi.AppUserMaintenance.AssignRole({
                                UserID: this.userID,
                                RoleID: this.roleID
                            })];
                    case 4:
                        _a.userRoleID = _b.sent();
                        this.assignedIcon();
                        _b.label = 5;
                    case 5: return [3 /*break*/, 7];
                    case 6:
                        this.enable();
                        this.icon.stopAnimation();
                        return [7 /*endfinally*/];
                    case 7: return [2 /*return*/];
                }
            });
        });
    };
    EditUserModifierListItem.prototype.withAssignedModifier = function (userRole) {
        this.modKey.setText(userRole.Role.Name);
        this.userRoleID = userRole.ID;
        this.roleID = userRole.ID;
        this.assignedIcon();
    };
    EditUserModifierListItem.prototype.assignedIcon = function () {
        this.icon.regularStyle();
        this.icon.setName('check-square');
        this.icon.setColor(ContextualClass_1.ContextualClass.success);
    };
    EditUserModifierListItem.prototype.withUnassignedModifier = function (modifier) {
        this.modKey.setText(modifier.ModKey);
        this.modDisplayText.setText(modifier.DisplayText);
        this.userRoleID = null;
        this.roleID = modifier.ID;
        this.unassignedIcon();
    };
    EditUserModifierListItem.prototype.unassignedIcon = function () {
        this.icon.regularStyle();
        this.icon.setName('square');
        this.icon.setColor(ContextualClass_1.ContextualClass.default);
    };
    return EditUserModifierListItem;
}(ButtonListGroupItem_1.ButtonListGroupItem));
exports.EditUserModifierListItem = EditUserModifierListItem;
//# sourceMappingURL=EditUserModifierListItem.js.map