"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Input_1 = require("../Html/Input");
var SimpleFieldFormGroup_1 = require("./SimpleFieldFormGroup");
var InputFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(InputFormGroup, _super);
    function InputFormGroup(prefix, name, vm, viewValue) {
        var _this = _super.call(this, prefix, name, vm) || this;
        _this.viewValue = viewValue;
        _this.input = _this.inputGroup.insertContent(0, new Input_1.Input())
            .configure(function (input) {
            input.addCssName('form-control');
        });
        var valueName = _this.getName();
        _this.input.setID(valueName);
        _this.input.setName(valueName);
        _this.input.changed.register(_this.onInputValueChanged.bind(_this));
        return _this;
    }
    InputFormGroup.prototype.onInputValueChanged = function (value) {
        this.viewValue.setValueFromView(value);
    };
    InputFormGroup.prototype.getValue = function () {
        return this.viewValue.getValue();
    };
    InputFormGroup.prototype.setValue = function (value) {
        this.viewValue.setValue(value);
        var inputValue = this.viewValue.toView();
        this.input.setValue(inputValue);
    };
    InputFormGroup.prototype.setMaxLength = function (maxLength) {
        this.input.setMaxLength(maxLength);
    };
    InputFormGroup.prototype.protect = function () {
        this.input.setType('password');
    };
    InputFormGroup.prototype.setFocus = function () { this.input.setFocus(); };
    InputFormGroup.prototype.blur = function () { this.input.blur(); };
    return InputFormGroup;
}(SimpleFieldFormGroup_1.SimpleFieldFormGroup));
exports.InputFormGroup = InputFormGroup;
//# sourceMappingURL=InputFormGroup.js.map