"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserAdminGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("../../Hub/AppApiGroup");
var UserAdminGroup = /** @class */ (function (_super) {
    tslib_1.__extends(UserAdminGroup, _super);
    function UserAdminGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'UserAdmin') || this;
        _this.AddUserAction = _this.createAction('AddUser', 'AddUser');
        return _this;
    }
    UserAdminGroup.prototype.AddUser = function (model, errorOptions) {
        return this.AddUserAction.execute(model, errorOptions || {});
    };
    return UserAdminGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UserAdminGroup = UserAdminGroup;
//# sourceMappingURL=UserAdminGroup.js.map