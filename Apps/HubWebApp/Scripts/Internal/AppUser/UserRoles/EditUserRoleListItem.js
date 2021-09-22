"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserRoleListItem = void 0;
var tslib_1 = require("tslib");
var FaIcon_1 = require("XtiShared/FaIcon");
var Row_1 = require("XtiShared/Grid/Row");
var ButtonListItemViewModel_1 = require("XtiShared/ListGroup/ButtonListItemViewModel");
var ButtonListGroupItem_1 = require("XtiShared/ListGroup/ButtonListGroupItem");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var ContextualClass_1 = require("XtiShared/ContextualClass");
var EditUserRoleListItem = /** @class */ (function (_super) {
    tslib_1.__extends(EditUserRoleListItem, _super);
    function EditUserRoleListItem(hubApi, vm) {
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
        _this.roleName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan(''));
        _this.clicked.register(_this.onClick.bind(_this));
        return _this;
    }
    EditUserRoleListItem.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    EditUserRoleListItem.prototype.onClick = function () {
        return this.toggleAssignment();
    };
    EditUserRoleListItem.prototype.toggleAssignment = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.disable();
                        this.icon.solidStyle();
                        this.icon.setName('sync-alt');
                        this.icon.startAnimation('spin');
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, , 3, 4]);
                        return [4 /*yield*/, this.hubApi.AppUserMaintenance.AssignRole({
                                UserID: this.userID,
                                RoleID: this.roleID
                            })];
                    case 2:
                        _a.sent();
                        this.assignedIcon();
                        return [3 /*break*/, 4];
                    case 3:
                        this.enable();
                        this.icon.stopAnimation();
                        return [7 /*endfinally*/];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    EditUserRoleListItem.prototype.withAssignedRole = function (userRole) {
        this.roleName.setText(userRole.Name);
        this.roleID = userRole.ID;
        this.assignedIcon();
    };
    EditUserRoleListItem.prototype.assignedIcon = function () {
        this.icon.regularStyle();
        this.icon.setName('check-square');
        this.icon.setColor(ContextualClass_1.ContextualClass.success);
    };
    EditUserRoleListItem.prototype.withUnassignedRole = function (role) {
        this.roleName.setText(role.Name);
        this.roleID = role.ID;
        this.unassignedIcon();
    };
    EditUserRoleListItem.prototype.unassignedIcon = function () {
        this.icon.regularStyle();
        this.icon.setName('square');
        this.icon.setColor(ContextualClass_1.ContextualClass.default);
    };
    return EditUserRoleListItem;
}(ButtonListGroupItem_1.ButtonListGroupItem));
exports.EditUserRoleListItem = EditUserRoleListItem;
//# sourceMappingURL=EditUserRoleListItem.js.map