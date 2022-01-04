"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var AppUserGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppUserGroup, _super);
    function AppUserGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'AppUser') || this;
        _this.Index = _this.createView('Index');
        _this.GetUserAccessAction = _this.createAction('GetUserAccess', 'Get User Access');
        _this.GetUnassignedRolesAction = _this.createAction('GetUnassignedRoles', 'Get Unassigned Roles');
        _this.GetUserModCategoriesAction = _this.createAction('GetUserModCategories', 'Get User Mod Categories');
        return _this;
    }
    AppUserGroup.prototype.GetUserAccess = function (model, errorOptions) {
        return this.GetUserAccessAction.execute(model, errorOptions || {});
    };
    AppUserGroup.prototype.GetUnassignedRoles = function (model, errorOptions) {
        return this.GetUnassignedRolesAction.execute(model, errorOptions || {});
    };
    AppUserGroup.prototype.GetUserModCategories = function (model, errorOptions) {
        return this.GetUserModCategoriesAction.execute(model, errorOptions || {});
    };
    return AppUserGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppUserGroup = AppUserGroup;
//# sourceMappingURL=AppUserGroup.js.map