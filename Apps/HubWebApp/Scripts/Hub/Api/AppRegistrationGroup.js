"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppRegistrationGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var AppRegistrationGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AppRegistrationGroup, _super);
    function AppRegistrationGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'AppRegistration') || this;
        _this.RegisterAppAction = _this.createAction('RegisterApp', 'Register App');
        _this.NewVersionAction = _this.createAction('NewVersion', 'New Version');
        _this.BeginPublishAction = _this.createAction('BeginPublish', 'Begin Publish');
        _this.EndPublishAction = _this.createAction('EndPublish', 'End Publish');
        _this.GetVersionsAction = _this.createAction('GetVersions', 'Get Versions');
        _this.GetVersionAction = _this.createAction('GetVersion', 'Get Version');
        _this.AddSystemUserAction = _this.createAction('AddSystemUser', 'Add System User');
        return _this;
    }
    AppRegistrationGroup.prototype.RegisterApp = function (model, errorOptions) {
        return this.RegisterAppAction.execute(model, errorOptions || {});
    };
    AppRegistrationGroup.prototype.NewVersion = function (model, errorOptions) {
        return this.NewVersionAction.execute(model, errorOptions || {});
    };
    AppRegistrationGroup.prototype.BeginPublish = function (model, errorOptions) {
        return this.BeginPublishAction.execute(model, errorOptions || {});
    };
    AppRegistrationGroup.prototype.EndPublish = function (model, errorOptions) {
        return this.EndPublishAction.execute(model, errorOptions || {});
    };
    AppRegistrationGroup.prototype.GetVersions = function (model, errorOptions) {
        return this.GetVersionsAction.execute(model, errorOptions || {});
    };
    AppRegistrationGroup.prototype.GetVersion = function (model, errorOptions) {
        return this.GetVersionAction.execute(model, errorOptions || {});
    };
    AppRegistrationGroup.prototype.AddSystemUser = function (model, errorOptions) {
        return this.AddSystemUserAction.execute(model, errorOptions || {});
    };
    return AppRegistrationGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppRegistrationGroup = AppRegistrationGroup;
//# sourceMappingURL=AppRegistrationGroup.js.map