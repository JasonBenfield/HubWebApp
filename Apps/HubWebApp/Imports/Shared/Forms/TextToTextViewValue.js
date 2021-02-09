"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var TypedFieldViewValue_1 = require("./TypedFieldViewValue");
var TextToTextViewValue = /** @class */ (function (_super) {
    tslib_1.__extends(TextToTextViewValue, _super);
    function TextToTextViewValue() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    TextToTextViewValue.prototype._fromView = function (value) {
        return value;
    };
    TextToTextViewValue.prototype._toView = function (value) {
        return value;
    };
    return TextToTextViewValue;
}(TypedFieldViewValue_1.TypedFieldViewValue));
exports.TextToTextViewValue = TextToTextViewValue;
//# sourceMappingURL=TextToTextViewValue.js.map