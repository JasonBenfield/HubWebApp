"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ComplexFieldLayout = void 0;
var BaseForm_1 = require("./BaseForm");
var ComplexFieldLayout = /** @class */ (function () {
    function ComplexFieldLayout(complexField) {
        this.complexField = complexField;
    }
    ComplexFieldLayout.prototype.execute = function () {
        this.executeLayout(this.complexField);
    };
    ComplexFieldLayout.prototype.executeLayout = function (complexField) {
        if (complexField instanceof BaseForm_1.BaseForm) {
            complexField.forEachFormGroup(function (fg) {
                fg.addToContainer(complexField);
            });
        }
        else {
            complexField.forEachFormGroup(function (fg) {
                fg.addToContainer(complexField.valueColumn);
            });
        }
    };
    return ComplexFieldLayout;
}());
exports.ComplexFieldLayout = ComplexFieldLayout;
//# sourceMappingURL=ComplexFieldLayout.js.map