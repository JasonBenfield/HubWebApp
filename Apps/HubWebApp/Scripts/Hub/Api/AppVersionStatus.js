"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppVersionStatus = exports.AppVersionStatuss = void 0;
var tslib_1 = require("tslib");
// Generated code
var NumericValue_1 = require("@jasonbenfield/sharedwebapp/NumericValue");
var NumericValues_1 = require("@jasonbenfield/sharedwebapp/NumericValues");
var AppVersionStatuss = /** @class */ (function (_super) {
    tslib_1.__extends(AppVersionStatuss, _super);
    function AppVersionStatuss(NotSet, New, Publishing, Old, Current) {
        var _this = _super.call(this, [NotSet, New, Publishing, Old, Current]) || this;
        _this.NotSet = NotSet;
        _this.New = New;
        _this.Publishing = Publishing;
        _this.Old = Old;
        _this.Current = Current;
        return _this;
    }
    return AppVersionStatuss;
}(NumericValues_1.NumericValues));
exports.AppVersionStatuss = AppVersionStatuss;
var AppVersionStatus = /** @class */ (function (_super) {
    tslib_1.__extends(AppVersionStatus, _super);
    function AppVersionStatus(Value, DisplayText) {
        return _super.call(this, Value, DisplayText) || this;
    }
    AppVersionStatus.values = new AppVersionStatuss(new AppVersionStatus(0, 'Not Set'), new AppVersionStatus(1, 'New'), new AppVersionStatus(2, 'Publishing'), new AppVersionStatus(3, 'Old'), new AppVersionStatus(4, 'Current'));
    return AppVersionStatus;
}(NumericValue_1.NumericValue));
exports.AppVersionStatus = AppVersionStatus;
//# sourceMappingURL=AppVersionStatus.js.map