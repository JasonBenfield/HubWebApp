"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/AppApiGroup");
var ResourceGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroup, _super);
    function ResourceGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Resource') || this;
        _this.GetResourceAction = _this.createAction('GetResource', 'Get Resource');
        _this.GetRoleAccessAction = _this.createAction('GetRoleAccess', 'Get Role Access');
        _this.GetMostRecentRequestsAction = _this.createAction('GetMostRecentRequests', 'Get Most Recent Requests');
        _this.GetMostRecentErrorEventsAction = _this.createAction('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
        return _this;
    }
    ResourceGroup.prototype.GetResource = function (model, errorOptions) {
        return this.GetResourceAction.execute(model, errorOptions || {});
    };
    ResourceGroup.prototype.GetRoleAccess = function (model, errorOptions) {
        return this.GetRoleAccessAction.execute(model, errorOptions || {});
    };
    ResourceGroup.prototype.GetMostRecentRequests = function (model, errorOptions) {
        return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
    };
    ResourceGroup.prototype.GetMostRecentErrorEvents = function (model, errorOptions) {
        return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
    };
    return ResourceGroup;
}(AppApiGroup_1.AppApiGroup));
exports.ResourceGroup = ResourceGroup;
//# sourceMappingURL=ResourceGroup.js.map