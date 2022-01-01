"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UsersGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var UsersGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UsersGroup, _super);
    function UsersGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Users') || this;
        _this.Index = _this.createView('Index');
        _this.GetUsersAction = _this.createAction('GetUsers', 'Get Users');
        _this.GetSystemUsersAction = _this.createAction('GetSystemUsers', 'Get System Users');
        _this.AddUserAction = _this.createAction('AddUser', 'Add User');
        return _this;
    }
    UsersGroup.prototype.GetUsers = function (errorOptions) {
        return this.GetUsersAction.execute({}, errorOptions || {});
    };
    UsersGroup.prototype.GetSystemUsers = function (model, errorOptions) {
        return this.GetSystemUsersAction.execute(model, errorOptions || {});
    };
    UsersGroup.prototype.AddUser = function (model, errorOptions) {
        return this.AddUserAction.execute(model, errorOptions || {});
    };
    return UsersGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UsersGroup = UsersGroup;
//# sourceMappingURL=UsersGroup.js.map