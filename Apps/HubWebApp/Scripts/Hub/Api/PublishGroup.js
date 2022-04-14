"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.PublishGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var PublishGroup = /** @class */ (function (_super) {
    tslib_1.__extends(PublishGroup, _super);
    function PublishGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Publish') || this;
        _this.NewVersionAction = _this.createAction('NewVersion', 'New Version');
        _this.BeginPublishAction = _this.createAction('BeginPublish', 'Begin Publish');
        _this.EndPublishAction = _this.createAction('EndPublish', 'End Publish');
        _this.GetVersionsAction = _this.createAction('GetVersions', 'Get Versions');
        return _this;
    }
    PublishGroup.prototype.NewVersion = function (model, errorOptions) {
        return this.NewVersionAction.execute(model, errorOptions || {});
    };
    PublishGroup.prototype.BeginPublish = function (model, errorOptions) {
        return this.BeginPublishAction.execute(model, errorOptions || {});
    };
    PublishGroup.prototype.EndPublish = function (model, errorOptions) {
        return this.EndPublishAction.execute(model, errorOptions || {});
    };
    PublishGroup.prototype.GetVersions = function (model, errorOptions) {
        return this.GetVersionsAction.execute(model, errorOptions || {});
    };
    return PublishGroup;
}(AppApiGroup_1.AppApiGroup));
exports.PublishGroup = PublishGroup;
//# sourceMappingURL=PublishGroup.js.map