"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceAccessCard = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var RoleAccessListItem_1 = require("../RoleAccessListItem");
var ResourceAccessCard = /** @class */ (function () {
    function ResourceAccessCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new CardTitleHeader_1.CardTitleHeader('Permissions', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.accessItems = new ListGroup_1.ListGroup(this.view.accessItems);
    }
    ResourceAccessCard.prototype.setResourceID = function (resourceID) {
        this.resourceID = resourceID;
    };
    ResourceAccessCard.prototype.refresh = function () {
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
    ResourceAccessCard.prototype.getRoleAccessItems = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var roles, accessItems, _i, roles_1, allowedRole;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Resource.GetRoleAccess({
                                            VersionKey: 'Current',
                                            ResourceID: this.resourceID
                                        })];
                                    case 1:
                                        roles = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        accessItems = [];
                        for (_i = 0, roles_1 = roles; _i < roles_1.length; _i++) {
                            allowedRole = roles_1[_i];
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
    return ResourceAccessCard;
}());
exports.ResourceAccessCard = ResourceAccessCard;
//# sourceMappingURL=ResourceAccessCard.js.map