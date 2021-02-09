"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var ConstraintCollection_1 = require("./ConstraintCollection");
var InputFormGroup_1 = require("./InputFormGroup");
var TextToNumberViewValue_1 = require("./TextToNumberViewValue");
var NumberInputFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(NumberInputFormGroup, _super);
    function NumberInputFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, prefix, name, vm, new TextToNumberViewValue_1.TextToNumberViewValue()) || this;
        _this.constraints = new ConstraintCollection_1.NumberConstraintCollection();
        return _this;
    }
    NumberInputFormGroup.prototype.validateConstraints = function (fieldErrors) {
        this.constraints.validate(fieldErrors, this);
    };
    return NumberInputFormGroup;
}(InputFormGroup_1.InputFormGroup));
exports.NumberInputFormGroup = NumberInputFormGroup;
//# sourceMappingURL=NumberInputFormGroup.js.map