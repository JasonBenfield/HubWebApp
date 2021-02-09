"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var ConstraintCollection_1 = require("./ConstraintCollection");
var DropDownFormGroup_1 = require("./DropDownFormGroup");
var DateDropDownFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(DateDropDownFormGroup, _super);
    function DateDropDownFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, prefix, name, vm) || this;
        _this.constraints = new ConstraintCollection_1.DateConstraintCollection();
        return _this;
    }
    DateDropDownFormGroup.prototype.validateConstraints = function (fieldErrors) {
        this.constraints.validate(fieldErrors, this);
    };
    return DateDropDownFormGroup;
}(DropDownFormGroup_1.DropDownFormGroup));
exports.DateDropDownFormGroup = DateDropDownFormGroup;
//# sourceMappingURL=DateDropDownFormGroup.js.map