"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.InstallGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var InstallGroup = /** @class */ (function (_super) {
    tslib_1.__extends(InstallGroup, _super);
    function InstallGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Install') || this;
        _this.RegisterAppAction = _this.createAction('RegisterApp', 'Register App');
        _this.GetVersionAction = _this.createAction('GetVersion', 'Get Version');
        _this.AddSystemUserAction = _this.createAction('AddSystemUser', 'Add System User');
        _this.NewInstallationAction = _this.createAction('NewInstallation', 'New Installation');
        _this.BeginCurrentInstallationAction = _this.createAction('BeginCurrentInstallation', 'Begin Current Installation');
        _this.BeginVersionInstallationAction = _this.createAction('BeginVersionInstallation', 'Begin Version Installation');
        _this.InstalledAction = _this.createAction('Installed', 'Installed');
        return _this;
    }
    InstallGroup.prototype.RegisterApp = function (model, errorOptions) {
        return this.RegisterAppAction.execute(model, errorOptions || {});
    };
    InstallGroup.prototype.GetVersion = function (model, errorOptions) {
        return this.GetVersionAction.execute(model, errorOptions || {});
    };
    InstallGroup.prototype.AddSystemUser = function (model, errorOptions) {
        return this.AddSystemUserAction.execute(model, errorOptions || {});
    };
    InstallGroup.prototype.NewInstallation = function (model, errorOptions) {
        return this.NewInstallationAction.execute(model, errorOptions || {});
    };
    InstallGroup.prototype.BeginCurrentInstallation = function (model, errorOptions) {
        return this.BeginCurrentInstallationAction.execute(model, errorOptions || {});
    };
    InstallGroup.prototype.BeginVersionInstallation = function (model, errorOptions) {
        return this.BeginVersionInstallationAction.execute(model, errorOptions || {});
    };
    InstallGroup.prototype.Installed = function (model, errorOptions) {
        return this.InstalledAction.execute(model, errorOptions || {});
    };
    return InstallGroup;
}(AppApiGroup_1.AppApiGroup));
exports.InstallGroup = InstallGroup;
//# sourceMappingURL=InstallGroup.js.map