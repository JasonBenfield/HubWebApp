"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserMaintenanceGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var UserMaintenanceGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserMaintenanceGroup, _super);
    function UserMaintenanceGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'UserMaintenance') || this;
        _this.EditUserAction = _this.createAction('EditUser', 'Edit User');
        _this.GetUserForEditAction = _this.createAction('GetUserForEdit', 'Get User For Edit');
        return _this;
    }
    UserMaintenanceGroup.prototype.EditUser = function (model, errorOptions) {
        return this.EditUserAction.execute(model, errorOptions || {});
    };
    UserMaintenanceGroup.prototype.GetUserForEdit = function (model, errorOptions) {
        return this.GetUserForEditAction.execute(model, errorOptions || {});
    };
    return UserMaintenanceGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UserMaintenanceGroup = UserMaintenanceGroup;
//# sourceMappingURL=UserMaintenanceGroup.js.map