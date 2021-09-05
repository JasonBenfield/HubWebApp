"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppVersionType = exports.AppVersionTypes = void 0;
var tslib_1 = require("tslib");
// Generated code
var NumericValue_1 = require("XtiShared/NumericValue");
var NumericValues_1 = require("XtiShared/NumericValues");
var AppVersionTypes = /** @class */ (function (_super) {
    tslib_1.__extends(AppVersionTypes, _super);
    function AppVersionTypes(NotSet, Major, Minor, Patch) {
        var _this = _super.call(this, [NotSet, Major, Minor, Patch]) || this;
        _this.NotSet = NotSet;
        _this.Major = Major;
        _this.Minor = Minor;
        _this.Patch = Patch;
        return _this;
    }
    return AppVersionTypes;
}(NumericValues_1.NumericValues));
exports.AppVersionTypes = AppVersionTypes;
var AppVersionType = /** @class */ (function (_super) {
    tslib_1.__extends(AppVersionType, _super);
    function AppVersionType(Value, DisplayText) {
        return _super.call(this, Value, DisplayText) || this;
    }
    AppVersionType.values = new AppVersionTypes(new AppVersionType(0, 'Not Set'), new AppVersionType(1, 'Major'), new AppVersionType(2, 'Minor'), new AppVersionType(3, 'Patch'));
    return AppVersionType;
}(NumericValue_1.NumericValue));
exports.AppVersionType = AppVersionType;
//# sourceMappingURL=AppVersionType.js.map