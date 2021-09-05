"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FormGroupCollection = void 0;
var BooleanDropDownFormGroup_1 = require("./BooleanDropDownFormGroup");
var ComplexFieldFormGroup_1 = require("./ComplexFieldFormGroup");
var DateDropDownFormGroup_1 = require("./DateDropDownFormGroup");
var DateInputFormGroup_1 = require("./DateInputFormGroup");
var DropDownFormGroup_1 = require("./DropDownFormGroup");
var NumberDropDownFormGroup_1 = require("./NumberDropDownFormGroup");
var NumberInputFormGroup_1 = require("./NumberInputFormGroup");
var TextDropDownFormGroup_1 = require("./TextDropDownFormGroup");
var TextInputFormGroup_1 = require("./TextInputFormGroup");
var FormGroupCollection = /** @class */ (function () {
    function FormGroupCollection(name) {
        this.name = name;
        this.values = [];
    }
    FormGroupCollection.prototype.formGroups = function () {
        return this.values;
    };
    FormGroupCollection.prototype.addHiddenTextFormGroup = function (name) {
        var formGroup = this.addTextInputFormGroup(name);
        this.hideFormGroup(formGroup);
        return formGroup;
    };
    FormGroupCollection.prototype.addHiddenNumberFormGroup = function (name) {
        var formGroup = this.addNumberInputFormGroup(name);
        this.hideFormGroup(formGroup);
        return formGroup;
    };
    FormGroupCollection.prototype.addHiddenDateFormGroup = function (name) {
        var formGroup = this.addDateInputFormGroup(name);
        this.hideFormGroup(formGroup);
        return formGroup;
    };
    FormGroupCollection.prototype.hideFormGroup = function (formGroup) {
        formGroup.input.setType('hidden');
        formGroup.hide();
    };
    FormGroupCollection.prototype.addTextInputFormGroup = function (name) {
        return this.addFormGroup(new TextInputFormGroup_1.TextInputFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addNumberInputFormGroup = function (name) {
        return this.addFormGroup(new NumberInputFormGroup_1.NumberInputFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addDateInputFormGroup = function (name) {
        return this.addFormGroup(new DateInputFormGroup_1.DateInputFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addTextDropDownFormGroup = function (name) {
        return this.addFormGroup(new TextDropDownFormGroup_1.TextDropDownFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addNumberDropDownFormGroup = function (name) {
        return this.addFormGroup(new NumberDropDownFormGroup_1.NumberDropDownFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addDateDropDownFormGroup = function (name) {
        return this.addFormGroup(new DateDropDownFormGroup_1.DateDropDownFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addBooleanDropDownFormGroup = function (name) {
        return this.addFormGroup(new BooleanDropDownFormGroup_1.BooleanDropDownFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addDropDownFormGroup = function (name) {
        return this.addFormGroup(new DropDownFormGroup_1.DropDownFormGroup(this.name, name));
    };
    FormGroupCollection.prototype.addFormGroup = function (formGroup) {
        this.values.push(formGroup);
        return formGroup;
    };
    FormGroupCollection.prototype.executeLayout = function () {
        this.forEach(function (fg) {
            if (fg instanceof ComplexFieldFormGroup_1.ComplexFieldFormGroup) {
                fg.executeLayout();
            }
        });
    };
    FormGroupCollection.prototype.forEach = function (action) {
        for (var _i = 0, _a = this.formGroups(); _i < _a.length; _i++) {
            var formGroup = _a[_i];
            action(formGroup);
        }
    };
    FormGroupCollection.prototype.getField = function (name) {
        var match = null;
        for (var _i = 0, _a = this.formGroups(); _i < _a.length; _i++) {
            var formGroup = _a[_i];
            var testField = formGroup.getField(name);
            if (testField) {
                match = testField;
                break;
            }
        }
        return match;
    };
    FormGroupCollection.prototype.clearErrors = function () {
        for (var _i = 0, _a = this.formGroups(); _i < _a.length; _i++) {
            var field = _a[_i];
            field.clearErrors();
        }
    };
    FormGroupCollection.prototype.validate = function (errors) {
        for (var _i = 0, _a = this.formGroups(); _i < _a.length; _i++) {
            var field = _a[_i];
            field.validate(errors);
        }
    };
    FormGroupCollection.prototype.import = function (values) {
        for (var _i = 0, _a = this.formGroups(); _i < _a.length; _i++) {
            var field = _a[_i];
            field.import(values);
        }
    };
    FormGroupCollection.prototype.export = function (values) {
        for (var _i = 0, _a = this.formGroups(); _i < _a.length; _i++) {
            var field = _a[_i];
            field.export(values);
        }
    };
    return FormGroupCollection;
}());
exports.FormGroupCollection = FormGroupCollection;
//# sourceMappingURL=FormGroupCollection.js.map