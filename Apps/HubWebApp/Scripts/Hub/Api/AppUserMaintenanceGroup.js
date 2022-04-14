"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserMaintenanceGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var AppUserMaintenanceGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AppUserMaintenanceGroup, _super);
    function AppUserMaintenanceGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'AppUserMaintenance') || this;
        _this.AssignRoleAction = _this.createAction('AssignRole', 'Assign Role');
        _this.UnassignRoleAction = _this.createAction('UnassignRole', 'Unassign Role');
        _this.DenyAccessAction = _this.createAction('DenyAccess', 'Deny Access');
        _this.AllowAccessAction = _this.createAction('AllowAccess', 'Allow Access');
        return _this;
    }
    AppUserMaintenanceGroup.prototype.AssignRole = function (model, errorOptions) {
        return this.AssignRoleAction.execute(model, errorOptions || {});
    };
    AppUserMaintenanceGroup.prototype.UnassignRole = function (model, errorOptions) {
        return this.UnassignRoleAction.execute(model, errorOptions || {});
    };
    AppUserMaintenanceGroup.prototype.DenyAccess = function (model, errorOptions) {
        return this.DenyAccessAction.execute(model, errorOptions || {});
    };
    AppUserMaintenanceGroup.prototype.AllowAccess = function (model, errorOptions) {
        return this.AllowAccessAction.execute(model, errorOptions || {});
    };
    return AppUserMaintenanceGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppUserMaintenanceGroup = AppUserMaintenanceGroup;
//# sourceMappingURL=AppUserMaintenanceGroup.js.map