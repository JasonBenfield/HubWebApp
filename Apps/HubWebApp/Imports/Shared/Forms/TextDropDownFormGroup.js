"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var ConstraintCollection_1 = require("./ConstraintCollection");
var DropDownFormGroup_1 = require("./DropDownFormGroup");
var TextDropDownFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(TextDropDownFormGroup, _super);
    function TextDropDownFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, prefix, name, vm) || this;
        _this.constraints = new ConstraintCollection_1.TextConstraintCollection();
        return _this;
    }
    TextDropDownFormGroup.prototype.validateConstraints = function (fieldErrors) {
        this.constraints.validate(fieldErrors, this);
    };
    return TextDropDownFormGroup;
}(DropDownFormGroup_1.DropDownFormGroup));
exports.TextDropDownFormGroup = TextDropDownFormGroup;
//# sourceMappingURL=TextDropDownFormGroup.js.map