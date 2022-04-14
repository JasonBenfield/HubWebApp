"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppType = exports.AppTypes = void 0;
var tslib_1 = require("tslib");
// Generated code
var NumericValue_1 = require("@jasonbenfield/sharedwebapp/NumericValue");
var NumericValues_1 = require("@jasonbenfield/sharedwebapp/NumericValues");
var AppTypes = /** @class */ (function (_super) {
    tslib_1.__extends(AppTypes, _super);
    function AppTypes(NotFound, WebApp, ServiceApp, Package, ConsoleApp) {
        var _this = _super.call(this, [NotFound, WebApp, ServiceApp, Package, ConsoleApp]) || this;
        _this.NotFound = NotFound;
        _this.WebApp = WebApp;
        _this.ServiceApp = ServiceApp;
        _this.Package = Package;
        _this.ConsoleApp = ConsoleApp;
        return _this;
    }
    return AppTypes;
}(NumericValues_1.NumericValues));
exports.AppTypes = AppTypes;
var AppType = /** @class */ (function (_super) {
    tslib_1.__extends(AppType, _super);
    function AppType(Value, DisplayText) {
        return _super.call(this, Value, DisplayText) || this;
    }
    AppType.values = new AppTypes(new AppType(0, 'Not Found'), new AppType(10, 'Web App'), new AppType(15, 'Service App'), new AppType(20, 'Package'), new AppType(25, 'Console App'));
    return AppType;
}(NumericValue_1.NumericValue));
exports.AppType = AppType;
//# sourceMappingURL=AppType.js.map