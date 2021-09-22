"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.VersionGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var VersionGroup = /** @class */ (function (_super) {
    tslib_1.__extends(VersionGroup, _super);
    function VersionGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Version') || this;
        _this.GetCurrentVersionAction = _this.createAction('GetCurrentVersion', 'Get Current Version');
        _this.GetVersionAction = _this.createAction('GetVersion', 'Get Version');
        _this.GetResourceGroupAction = _this.createAction('GetResourceGroup', 'Get Resource Group');
        return _this;
    }
    VersionGroup.prototype.GetCurrentVersion = function (errorOptions) {
        return this.GetCurrentVersionAction.execute({}, errorOptions || {});
    };
    VersionGroup.prototype.GetVersion = function (model, errorOptions) {
        return this.GetVersionAction.execute(model, errorOptions || {});
    };
    VersionGroup.prototype.GetResourceGroup = function (model, errorOptions) {
        return this.GetResourceGroupAction.execute(model, errorOptions || {});
    };
    return VersionGroup;
}(AppApiGroup_1.AppApiGroup));
exports.VersionGroup = VersionGroup;
//# sourceMappingURL=VersionGroup.js.map