"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppEventSeverity = exports.AppEventSeveritys = void 0;
var tslib_1 = require("tslib");
// Generated code
var NumericValue_1 = require("@jasonbenfield/sharedwebapp/NumericValue");
var NumericValues_1 = require("@jasonbenfield/sharedwebapp/NumericValues");
var AppEventSeveritys = /** @class */ (function (_super) {
    tslib_1.__extends(AppEventSeveritys, _super);
    function AppEventSeveritys(NotSet, CriticalError, AccessDenied, AppError, ValidationFailed, Information) {
        var _this = _super.call(this, [NotSet, CriticalError, AccessDenied, AppError, ValidationFailed, Information]) || this;
        _this.NotSet = NotSet;
        _this.CriticalError = CriticalError;
        _this.AccessDenied = AccessDenied;
        _this.AppError = AppError;
        _this.ValidationFailed = ValidationFailed;
        _this.Information = Information;
        return _this;
    }
    return AppEventSeveritys;
}(NumericValues_1.NumericValues));
exports.AppEventSeveritys = AppEventSeveritys;
var AppEventSeverity = /** @class */ (function (_super) {
    tslib_1.__extends(AppEventSeverity, _super);
    function AppEventSeverity(Value, DisplayText) {
        return _super.call(this, Value, DisplayText) || this;
    }
    AppEventSeverity.values = new AppEventSeveritys(new AppEventSeverity(0, 'Not Set'), new AppEventSeverity(100, 'Critical Error'), new AppEventSeverity(80, 'Access Denied'), new AppEventSeverity(70, 'App Error'), new AppEventSeverity(60, 'Validation Failed'), new AppEventSeverity(50, 'Information'));
    return AppEventSeverity;
}(NumericValue_1.NumericValue));
exports.AppEventSeverity = AppEventSeverity;
//# sourceMappingURL=AppEventSeverity.js.map