"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ConstraintCollection_1 = require("./ConstraintCollection");
var ErrorList_1 = require("./ErrorList");
var HiddenField = /** @class */ (function () {
    function HiddenField(prefix, name, vm) {
        this.vm = vm;
        this.constraints = new ConstraintCollection_1.ConstraintCollection();
        this.name = prefix ? prefix + "_" + name : name;
        this.vm.name(this.name);
    }
    HiddenField.prototype.getName = function () {
        return this.name;
    };
    HiddenField.prototype.setCaption = function (caption) {
        this.caption = caption;
    };
    HiddenField.prototype.getCaption = function () {
        return this.caption;
    };
    HiddenField.prototype.getField = function (name) {
        return this.getName() === name ? this : null;
    };
    HiddenField.prototype.setValue = function (value) { this.value = value; };
    HiddenField.prototype.getValue = function () {
        return this.value;
    };
    HiddenField.prototype.clearErrors = function () {
    };
    HiddenField.prototype.validate = function (errors) {
        var fieldErrors = new ErrorList_1.ErrorList();
        this.constraints.validate(fieldErrors, this);
        errors.merge(fieldErrors);
    };
    HiddenField.prototype.import = function (values) {
        if (values) {
            var value = values[this.getName()];
            if (value !== undefined) {
                this.setValue(value);
            }
        }
    };
    HiddenField.prototype.export = function (values) {
        values[this.getName()] = this.getValue();
    };
    return HiddenField;
}());
exports.HiddenField = HiddenField;
//# sourceMappingURL=HiddenField.js.map