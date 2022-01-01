"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupAccessCard = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var RoleAccessListItem_1 = require("../RoleAccessListItem");
var ResourceGroupAccessCard = /** @class */ (function () {
    function ResourceGroupAccessCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Permissions', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
    }
    ResourceGroupAccessCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceGroupAccessCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var accessItems;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getRoleAccessItems()];
                    case 1:
                        accessItems = _a.sent();
                        this.accessItems.setItems(accessItems, function (sourceItem, listItem) {
                            return new RoleAccessListItem_1.RoleAccessListItem(sourceItem, listItem);
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
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var allowedRoles, accessItems, _i, allowedRoles_1, allowedRole;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetRoleAccess({
                                            VersionKey: 'Current',
                                            GroupID: this.groupID
                                        })];
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
}());
exports.ResourceGroupAccessCard = ResourceGroupAccessCard;
//# sourceMappingURL=ResourceGroupAccessCard.js.map