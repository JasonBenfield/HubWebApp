"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ErrorModel = void 0;
var ErrorModel = /** @class */ (function () {
    function ErrorModel(message, propertyName, context) {
        if (propertyName === void 0) { propertyName = ''; }
        this.message = message;
        this.propertyName = propertyName;
        this.context = context;
    }
    ErrorModel.prototype.toString = function () {
        var str = '';
        if (this.propertyName) {
            str += this.propertyName + ", ";
        }
        str += this.message;
        return str;
    };
    return ErrorModel;
}());
exports.ErrorModel = ErrorModel;
//# sourceMappingURL=ErrorModel.js.map