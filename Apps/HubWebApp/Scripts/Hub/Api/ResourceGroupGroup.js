"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var ResourceGroupGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroupGroup, _super);
    function ResourceGroupGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'ResourceGroup') || this;
        _this.GetResourceGroupAction = _this.createAction('GetResourceGroup', 'Get Resource Group');
        _this.GetResourcesAction = _this.createAction('GetResources', 'Get Resources');
        _this.GetRoleAccessAction = _this.createAction('GetRoleAccess', 'Get Role Access');
        _this.GetModCategoryAction = _this.createAction('GetModCategory', 'Get Mod Category');
        _this.GetMostRecentRequestsAction = _this.createAction('GetMostRecentRequests', 'Get Most Recent Requests');
        _this.GetMostRecentErrorEventsAction = _this.createAction('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
        return _this;
    }
    ResourceGroupGroup.prototype.GetResourceGroup = function (model, errorOptions) {
        return this.GetResourceGroupAction.execute(model, errorOptions || {});
    };
    ResourceGroupGroup.prototype.GetResources = function (model, errorOptions) {
        return this.GetResourcesAction.execute(model, errorOptions || {});
    };
    ResourceGroupGroup.prototype.GetRoleAccess = function (model, errorOptions) {
        return this.GetRoleAccessAction.execute(model, errorOptions || {});
    };
    ResourceGroupGroup.prototype.GetModCategory = function (model, errorOptions) {
        return this.GetModCategoryAction.execute(model, errorOptions || {});
    };
    ResourceGroupGroup.prototype.GetMostRecentRequests = function (model, errorOptions) {
        return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
    };
    ResourceGroupGroup.prototype.GetMostRecentErrorEvents = function (model, errorOptions) {
        return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
    };
    return ResourceGroupGroup;
}(AppApiGroup_1.AppApiGroup));
exports.ResourceGroupGroup = ResourceGroupGroup;
//# sourceMappingURL=ResourceGroupGroup.js.map