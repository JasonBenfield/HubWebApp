"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UsersGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var UsersGroup = /** @class */ (function (_super) {
    tslib_1.__extends(UsersGroup, _super);
    function UsersGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Users') || this;
        _this.Index = _this.createView('Index');
        _this.GetUsersAction = _this.createAction('GetUsers', 'Get Users');
        return _this;
    }
    UsersGroup.prototype.GetUsers = function (errorOptions) {
        return this.GetUsersAction.execute({}, errorOptions || {});
    };
    return UsersGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UsersGroup = UsersGroup;
//# sourceMappingURL=UsersGroup.js.map