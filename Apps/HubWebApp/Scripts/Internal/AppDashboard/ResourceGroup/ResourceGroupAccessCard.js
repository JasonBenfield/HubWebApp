"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupAccessCard = void 0;
var tslib_1 = require("tslib");
var ListCard_1 = require("../ListCard");
var RoleAccessListItemViewModel_1 = require("../RoleAccessListItemViewModel");
var ResourceGroupAccessCard = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupAccessCard, _super);
    function ResourceGroupAccessCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Roles were Found') || this;
        _this.hubApi = hubApi;
        vm.title('Permissions');
        return _this;
    }
    ResourceGroupAccessCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceGroupAccessCard.prototype.createItem = function (sourceItem) {
        var item = new RoleAccessListItemViewModel_1.RoleAccessListItemViewModel();
        item.roleName(sourceItem.role.Name);
        item.isAllowed(sourceItem.isAllowed);
        return item;
    };
    ResourceGroupAccessCard.prototype.getSourceItems = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var access, accessItems, _i, _a, allowedRole, _b, _c, deniedRole;
            return tslib_1.__generator(this, function (_d) {
                switch (_d.label) {
                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetRoleAccess(this.groupID)];
                    case 1:
                        access = _d.sent();
                        accessItems = [];
                        for (_i = 0, _a = access.AllowedRoles; _i < _a.length; _i++) {
                            allowedRole = _a[_i];
                            accessItems.push({
                                isAllowed: true,
                                role: allowedRole
                            });
                        }
                        for (_b = 0, _c = access.DeniedRoles; _b < _c.length; _b++) {
                            deniedRole = _c[_b];
                            accessItems.push({
                                isAllowed: false,
                                role: deniedRole
                            });
                        }
                        return [2 /*return*/, accessItems];
                }
            });
        });
    };
    return ResourceGroupAccessCard;
}(ListCard_1.ListCard));
exports.ResourceGroupAccessCard = ResourceGroupAccessCard;
//# sourceMappingURL=ResourceGroupAccessCard.js.map