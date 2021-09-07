"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppsGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var AppsGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AppsGroup, _super);
    function AppsGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Apps') || this;
        _this.Index = _this.createView('Index');
        _this.AllAction = _this.createAction('All', 'All');
        _this.GetAppModifierKeyAction = _this.createAction('GetAppModifierKey', 'Get App Modifier Key');
        _this.RedirectToApp = _this.createView('RedirectToApp');
        return _this;
    }
    AppsGroup.prototype.All = function (errorOptions) {
        return this.AllAction.execute({}, errorOptions || {});
    };
    AppsGroup.prototype.GetAppModifierKey = function (model, errorOptions) {
        return this.GetAppModifierKeyAction.execute(model, errorOptions || {});
    };
    return AppsGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AppsGroup = AppsGroup;
//# sourceMappingURL=AppsGroup.js.map