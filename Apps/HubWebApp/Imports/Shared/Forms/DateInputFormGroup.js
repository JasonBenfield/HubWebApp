"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var ConstraintCollection_1 = require("./ConstraintCollection");
var InputFormGroup_1 = require("./InputFormGroup");
var TextToDateViewValue_1 = require("./TextToDateViewValue");
var DateInputFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(DateInputFormGroup, _super);
    function DateInputFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, prefix, name, vm, new TextToDateViewValue_1.TextToDateViewValue()) || this;
        _this.constraints = new ConstraintCollection_1.DateConstraintCollection();
        _this.input.setType('date');
        return _this;
    }
    DateInputFormGroup.prototype.validateConstraints = function (fieldErrors) {
        this.constraints.validate(fieldErrors, this);
    };
    return DateInputFormGroup;
}(InputFormGroup_1.InputFormGroup));
exports.DateInputFormGroup = DateInputFormGroup;
//# sourceMappingURL=DateInputFormGroup.js.map