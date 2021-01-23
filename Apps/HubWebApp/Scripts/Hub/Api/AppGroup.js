"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var AppGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AppGroup, _super);
    function AppGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'App') || this;
        _this.Index = _this.createView('Index');
        _this.GetAppAction = _this.createAction('GetApp', 'Get App');
        _this.GetCurrentVersionAction = _this.createAction('GetCurrentVersion', 'Get Current Version');
        _this.GetResourceGroupsAction = _this.createAction('GetResourceGroups', 'Get Resource Groups');
        _this.GetMostRecentRequestsAction = _this.createAction('GetMostRecentRequests', 'Get Most Recent Requests');
        _this.GetMostRecentErrorEventsAction = _this.createAction('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
        _this.GetModifierCategoriesAction = _this.createAction('GetModifierCategories', 'Get Modifier Categories');
        return _this;
    }
    AppGroup.prototype.GetApp = function (errorOptions) {
        return this.GetAppAction.execute({}, errorOptions || {});
    };
    AppGroup.prototype.GetCurrentVersion = function (errorOptions) {
        return this.GetCurrentVersionAction.execute({}, errorOptions || {});
    };
    AppGroup.prototype.GetResourceGroups = function (errorOptions) {
        return this.GetResourceGroupsAction.execute({}, errorOptions || {});
    };
    AppGroup.prototype.GetMostRecentRequests = function (model, errorOptions) {
        return this.GetMostRecentRequestsAction.execute(model, errorOptions || {});
    };
    AppGroup.prototype.GetMostRecentErrorEvents = function (model, errorOptions) {
        return this.GetMostRecentErrorEventsAction.execute(model, errorOptions || {});
    };
    AppGroup.prototype.GetModifierCategories = function (errorOptions) {
        return this.GetModifierCategoriesAction.execute({}, errorOptions || {});
    };
    return AppGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppGroup = AppGroup;
//# sourceMappingURL=AppGroup.js.map