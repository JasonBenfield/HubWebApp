"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupAccessCard = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var RoleAccessListItem_1 = require("../RoleAccessListItem");
var ResourceGroupAccessCard = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupAccessCard, _super);
    function ResourceGroupAccessCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Permissions');
        _this.alert = _this.addCardAlert().alert;
        return _this;
    }
    ResourceGroupAccessCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceGroupAccessCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var accessItems;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getRoleAccessItems()];
                    case 1:
                        accessItems = _a.sent();
                        this.accessItems.setItems(accessItems, function (sourceItem, listItem) {
                            listItem.addContent(new RoleAccessListItem_1.RoleAccessListItem(sourceItem));
                        });
                        if (accessItems.length === 0) {
                            this.alert.danger('No Roles were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceGroupAccessCard.prototype.getRoleAccessItems = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var allowedRoles, accessItems, _i, allowedRoles_1, allowedRole;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetRoleAccess(this.groupID)];
                                    case 1:
                                        allowedRoles = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        accessItems = [];
                        for (_i = 0, allowedRoles_1 = allowedRoles; _i < allowedRoles_1.length; _i++) {
                            allowedRole = allowedRoles_1[_i];
                            accessItems.push({
                                isAllowed: true,
                                role: allowedRole
                            });
                        }
                        return [2 /*return*/, accessItems];
                }
            });
        });
    };
    return ResourceGroupAccessCard;
}(Card_1.Card));
exports.ResourceGroupAccessCard = ResourceGroupAccessCard;
//# sourceMappingURL=ResourceGroupAccessCard.js.map