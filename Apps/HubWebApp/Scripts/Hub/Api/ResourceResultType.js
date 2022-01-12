"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceResultType = exports.ResourceResultTypes = void 0;
var tslib_1 = require("tslib");
// Generated code
var NumericValue_1 = require("@jasonbenfield/sharedwebapp/NumericValue");
var NumericValues_1 = require("@jasonbenfield/sharedwebapp/NumericValues");
var ResourceResultTypes = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceResultTypes, _super);
    function ResourceResultTypes(None, View, PartialView, Redirect, Json) {
        var _this = _super.call(this, [None, View, PartialView, Redirect, Json]) || this;
        _this.None = None;
        _this.View = View;
        _this.PartialView = PartialView;
        _this.Redirect = Redirect;
        _this.Json = Json;
        return _this;
    }
    return ResourceResultTypes;
}(NumericValues_1.NumericValues));
exports.ResourceResultTypes = ResourceResultTypes;
var ResourceResultType = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceResultType, _super);
    function ResourceResultType(Value, DisplayText) {
        return _super.call(this, Value, DisplayText) || this;
    }
    ResourceResultType.values = new ResourceResultTypes(new ResourceResultType(0, 'None'), new ResourceResultType(1, 'View'), new ResourceResultType(2, 'PartialView'), new ResourceResultType(3, 'Redirect'), new ResourceResultType(4, 'Json'));
    return ResourceResultType;
}(NumericValue_1.NumericValue));
exports.ResourceResultType = ResourceResultType;
//# sourceMappingURL=ResourceResultType.js.map