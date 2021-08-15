"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var Select_1 = require("../Html/Select");
var ConstraintCollection_1 = require("./ConstraintCollection");
var SimpleFieldFormGroup_1 = require("./SimpleFieldFormGroup");
var DropDownFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(DropDownFormGroup, _super);
    function DropDownFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, prefix, name, vm) || this;
        _this.constraints = new ConstraintCollection_1.ConstraintCollection();
        _this.select = _this.inputGroup.insertContent(0, new Select_1.Select())
            .configure(function (select) {
            select.addCssName('form-control');
        });
        _this.valueChanged = _this.select.changed;
        return _this;
    }
    DropDownFormGroup.prototype.validateConstraints = function (fieldErrors) {
        this.constraints.validate(fieldErrors, this);
    };
    DropDownFormGroup.prototype.getValue = function () {
        return this.select.getValue();
    };
    DropDownFormGroup.prototype.setValue = function (value) {
        this.select.setValue(value);
    };
    DropDownFormGroup.prototype.setItems = function () {
        var items = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            items[_i] = arguments[_i];
        }
        this.select.setItems(items);
    };
    DropDownFormGroup.prototype.setItemCaption = function (itemCaption) {
        this.select.setItemCaption(itemCaption);
    };
    return DropDownFormGroup;
}(SimpleFieldFormGroup_1.SimpleFieldFormGroup));
exports.DropDownFormGroup = DropDownFormGroup;
//# sourceMappingURL=DropDownFormGroup.js.map