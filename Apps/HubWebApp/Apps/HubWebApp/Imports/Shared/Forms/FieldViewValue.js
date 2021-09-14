"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FieldViewValue = void 0;
var FieldViewValue = /** @class */ (function () {
    function FieldViewValue() {
    }
    FieldViewValue.prototype.getValue = function () {
        return this.value;
    };
    FieldViewValue.prototype.setValue = function (value) {
        this.value = value;
    };
    FieldViewValue.prototype.setValueFromView = function (viewValue) {
        var value = this._fromView(viewValue);
        this.value = value;
        return value;
    };
    FieldViewValue.prototype._fromView = function (value) {
        return value;
    };
    FieldViewValue.prototype.toView = function () {
        return this._toView(this.value);
    };
    FieldViewValue.prototype._toView = function (value) { return value; };
    return FieldViewValue;
}());
exports.FieldViewValue = FieldViewValue;
//# sourceMappingURL=FieldViewValue.js.map