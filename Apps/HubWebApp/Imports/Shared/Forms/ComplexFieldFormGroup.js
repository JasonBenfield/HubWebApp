"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var FormGroup_1 = require("../Html/FormGroup");
var ComplexFieldLayout_1 = require("./ComplexFieldLayout");
var FormGroupCollection_1 = require("./FormGroupCollection");
var ComplexFieldFormGroup = /** @class */ (function (_super) {
    tslib_1.__extends(ComplexFieldFormGroup, _super);
    function ComplexFieldFormGroup(prefix, name, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.layout = new ComplexFieldLayout_1.ComplexFieldLayout(_this);
        _this.name = prefix ? prefix + "_" + name : name;
        _this.formGroups = new FormGroupCollection_1.FormGroupCollection(_this.name);
        return _this;
    }
    ComplexFieldFormGroup.prototype.useLayout = function (createLayout) {
        this.layout = createLayout(this);
    };
    ComplexFieldFormGroup.prototype.executeLayout = function () {
        this.layout.execute();
        this.formGroups.executeLayout();
    };
    ComplexFieldFormGroup.prototype.getName = function () {
        return this.name;
    };
    ComplexFieldFormGroup.prototype.setValue = function (_) {
    };
    ComplexFieldFormGroup.prototype.getValue = function () {
        return null;
    };
    ComplexFieldFormGroup.prototype.getField = function (name) {
        if (this.getName() === name) {
            return this;
        }
        return this.formGroups.getField(name);
    };
    ComplexFieldFormGroup.prototype.forEachFormGroup = function (action) {
        this.formGroups.forEach(action);
    };
    ComplexFieldFormGroup.prototype.addHiddenTextFormGroup = function (name) {
        return this.formGroups.addHiddenTextFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addHiddenNumberFormGroup = function (name) {
        return this.formGroups.addHiddenNumberFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addHiddenDateFormGroup = function (name) {
        return this.formGroups.addHiddenDateFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addTextInputFormGroup = function (name) {
        return this.formGroups.addTextInputFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addNumberInputFormGroup = function (name) {
        return this.formGroups.addNumberInputFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addDateInputFormGroup = function (name) {
        return this.formGroups.addDateInputFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addTextDropDownFormGroup = function (name) {
        return this.formGroups.addTextDropDownFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addNumberDropDownFormGroup = function (name) {
        return this.formGroups.addNumberDropDownFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addDateDropDownFormGroup = function (name) {
        return this.formGroups.addDateDropDownFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addBooleanDropDownFormGroup = function (name) {
        return this.formGroups.addBooleanDropDownFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addDropDownFormGroup = function (name) {
        return this.formGroups.addDropDownFormGroup(name);
    };
    ComplexFieldFormGroup.prototype.addFormGroup = function (name, formGroup) {
        return this.formGroups.addFormGroup(formGroup);
    };
    ComplexFieldFormGroup.prototype.clearErrors = function () {
        this.formGroups.clearErrors();
    };
    ComplexFieldFormGroup.prototype.validate = function (errors) {
        this.formGroups.validate(errors);
    };
    ComplexFieldFormGroup.prototype.import = function (values) {
        if (values) {
            this.formGroups.import(values);
        }
    };
    ComplexFieldFormGroup.prototype.export = function (values) {
        this.formGroups.export(values);
    };
    return ComplexFieldFormGroup;
}(FormGroup_1.FormGroup));
exports.ComplexFieldFormGroup = ComplexFieldFormGroup;
//# sourceMappingURL=ComplexFieldFormGroup.js.map