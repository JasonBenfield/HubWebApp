"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BooleanDropDownFormGroup = void 0;
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var ConstraintCollection_1 = require("./ConstraintCollection");
var DropDownFormGroup_1 = require("./DropDownFormGroup");
var BooleanDropDownFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(BooleanDropDownFormGroup, _super);
    function BooleanDropDownFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, prefix, name, vm) || this;
        _this.constraints = new ConstraintCollection_1.ConstraintCollection();
        return _this;
    }
    BooleanDropDownFormGroup.prototype.validateConstraints = function (fieldErrors) {
        this.constraints.validate(fieldErrors, this);
    };
    return BooleanDropDownFormGroup;
}(DropDownFormGroup_1.DropDownFormGroup));
exports.BooleanDropDownFormGroup = BooleanDropDownFormGroup;
//# sourceMappingURL=BooleanDropDownFormGroup.js.map