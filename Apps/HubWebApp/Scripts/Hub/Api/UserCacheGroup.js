"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserCacheGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var UserCacheGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserCacheGroup, _super);
    function UserCacheGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'UserCache') || this;
        _this.ClearCacheAction = _this.createAction('ClearCache', 'Clear Cache');
        return _this;
    }
    UserCacheGroup.prototype.ClearCache = function (model, errorOptions) {
        return this.ClearCacheAction.execute(model, errorOptions || {});
    };
    return UserCacheGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UserCacheGroup = UserCacheGroup;
//# sourceMappingURL=UserCacheGroup.js.map