"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppsGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var AppsGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppsGroup, _super);
    function AppsGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Apps') || this;
        _this.Index = _this.createView('Index');
        _this.GetAppsAction = _this.createAction('GetApps', 'Get Apps');
        _this.GetAppByIdAction = _this.createAction('GetAppById', 'Get App By Id');
        _this.GetAppByAppKeyAction = _this.createAction('GetAppByAppKey', 'Get App By App Key');
        _this.RedirectToApp = _this.createView('RedirectToApp');
        return _this;
    }
    AppsGroup.prototype.GetApps = function (errorOptions) {
        return this.GetAppsAction.execute({}, errorOptions || {});
    };
    AppsGroup.prototype.GetAppById = function (model, errorOptions) {
        return this.GetAppByIdAction.execute(model, errorOptions || {});
    };
    AppsGroup.prototype.GetAppByAppKey = function (model, errorOptions) {
        return this.GetAppByAppKeyAction.execute(model, errorOptions || {});
    };
    return AppsGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppsGroup = AppsGroup;
//# sourceMappingURL=AppsGroup.js.map