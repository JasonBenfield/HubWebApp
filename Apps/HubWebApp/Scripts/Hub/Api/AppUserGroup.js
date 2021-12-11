"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var AppUserGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppUserGroup, _super);
    function AppUserGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'AppUser') || this;
        _this.Index = _this.createView('Index');
        _this.GetUserRolesAction = _this.createAction('GetUserRoles', 'Get User Roles');
        _this.GetUserRoleAccessAction = _this.createAction('GetUserRoleAccess', 'Get User Role Access');
        _this.GetUserModCategoriesAction = _this.createAction('GetUserModCategories', 'Get User Mod Categories');
        return _this;
    }
    AppUserGroup.prototype.GetUserRoles = function (model, errorOptions) {
        return this.GetUserRolesAction.execute(model, errorOptions || {});
    };
    AppUserGroup.prototype.GetUserRoleAccess = function (model, errorOptions) {
        return this.GetUserRoleAccessAction.execute(model, errorOptions || {});
    };
    AppUserGroup.prototype.GetUserModCategories = function (model, errorOptions) {
        return this.GetUserModCategoriesAction.execute(model, errorOptions || {});
    };
    return AppUserGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppUserGroup = AppUserGroup;
//# sourceMappingURL=AppUserGroup.js.map