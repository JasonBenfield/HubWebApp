"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var AppGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AppGroup, _super);
    function AppGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'App') || this;
        _this.Index = _this.createView('Index');
        _this.GetAppAction = _this.createAction('GetApp', 'Get App');
        _this.GetRolesAction = _this.createAction('GetRoles', 'Get Roles');
        _this.GetRoleAction = _this.createAction('GetRole', 'Get Role');
        _this.GetResourceGroupsAction = _this.createAction('GetResourceGroups', 'Get Resource Groups');
        _this.GetMostRecentRequestsAction = _this.createAction('GetMostRecentRequests', 'Get Most Recent Requests');
        _this.GetMostRecentErrorEventsAction = _this.createAction('GetMostRecentErrorEvents', 'Get Most Recent Error Events');
        _this.GetModifierCategoriesAction = _this.createAction('GetModifierCategories', 'Get Modifier Categories');
        _this.GetModifierCategoryAction = _this.createAction('GetModifierCategory', 'Get Modifier Category');
        _this.GetDefaultModifierAction = _this.createAction('GetDefaultModifier', 'Get Default Modifier');
        return _this;
    }
    AppGroup.prototype.GetApp = function (errorOptions) {
        return this.GetAppAction.execute({}, errorOptions || {});
    };
    AppGroup.prototype.GetRoles = function (errorOptions) {
        return this.GetRolesAction.execute({}, errorOptions || {});
    };
    AppGroup.prototype.GetRole = function (model, errorOptions) {
        return this.GetRoleAction.execute(model, errorOptions || {});
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
    AppGroup.prototype.GetModifierCategory = function (model, errorOptions) {
        return this.GetModifierCategoryAction.execute(model, errorOptions || {});
    };
    AppGroup.prototype.GetDefaultModifier = function (errorOptions) {
        return this.GetDefaultModifierAction.execute({}, errorOptions || {});
    };
    return AppGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppGroup = AppGroup;
//# sourceMappingURL=AppGroup.js.map