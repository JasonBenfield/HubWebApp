"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var AppApiError_1 = require("../AppApiError");
var ConsoleLog_1 = require("../ConsoleLog");
var ModalErrorComponent_1 = require("../Error/ModalErrorComponent");
var ErrorModel_1 = require("../ErrorModel");
var ErrorList_1 = require("./ErrorList");
var FormGroupCollection_1 = require("./FormGroupCollection");
var FormSaveResult_1 = require("./FormSaveResult");
var FormComponentViewModel_1 = require("../Html/FormComponentViewModel");
var FormComponent_1 = require("../Html/FormComponent");
var ComplexFieldLayout_1 = require("./ComplexFieldLayout");
var BaseForm = /** @class */ (function (_super) {
    tslib_1.__extends(BaseForm, _super);
    function BaseForm(name, vm) {
        if (vm === void 0) { vm = new FormComponentViewModel_1.FormComponentViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.name = name;
        _this.layout = new ComplexFieldLayout_1.ComplexFieldLayout(_this);
        _this.formGroups = new FormGroupCollection_1.FormGroupCollection(_this.name);
        _this.modalError = new ModalErrorComponent_1.ModalErrorComponent().addToContainer(_this.content);
        _this.modalError.errorSelected.register(_this.onErrorSelected.bind(_this));
        return _this;
    }
    BaseForm.prototype.onErrorSelected = function (error) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var field;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.modalError.okCommand.execute()];
                    case 1:
                        _a.sent();
                        field = this.getField(error.Source);
                        if (field) {
                            if (field.setFocus) {
                                field.setFocus();
                            }
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    BaseForm.prototype.useLayout = function (layout) {
        this.layout = layout;
    };
    BaseForm.prototype.executeLayout = function () {
        this.layout.execute();
        this.formGroups.executeLayout();
    };
    BaseForm.prototype.forEachFormGroup = function (action) {
        this.formGroups.forEach(action);
    };
    BaseForm.prototype.getName = function () { return this.name; };
    BaseForm.prototype.getField = function (name) {
        if (name) {
            if (this.getName() === name) {
                return this;
            }
            return this.formGroups.getField(name);
        }
        return null;
    };
    BaseForm.prototype.addHiddenTextFormGroup = function (name) {
        return this.formGroups.addHiddenTextFormGroup(name);
    };
    BaseForm.prototype.addHiddenNumberFormGroup = function (name) {
        return this.formGroups.addHiddenNumberFormGroup(name);
    };
    BaseForm.prototype.addHiddenDateFormGroup = function (name) {
        return this.formGroups.addHiddenDateFormGroup(name);
    };
    BaseForm.prototype.addTextInputFormGroup = function (name) {
        return this.formGroups.addTextInputFormGroup(name);
    };
    BaseForm.prototype.addNumberInputFormGroup = function (name) {
        return this.formGroups.addNumberInputFormGroup(name);
    };
    BaseForm.prototype.addDateInputFormGroup = function (name) {
        return this.formGroups.addDateInputFormGroup(name);
    };
    BaseForm.prototype.addTextDropDownFormGroup = function (name) {
        return this.formGroups.addTextDropDownFormGroup(name);
    };
    BaseForm.prototype.addNumberDropDownFormGroup = function (name) {
        return this.formGroups.addNumberDropDownFormGroup(name);
    };
    BaseForm.prototype.addDateDropDownFormGroup = function (name) {
        return this.formGroups.addDateDropDownFormGroup(name);
    };
    BaseForm.prototype.addBooleanDropDownFormGroup = function (name) {
        return this.formGroups.addBooleanDropDownFormGroup(name);
    };
    BaseForm.prototype.addDropDownFormGroup = function (name) {
        return this.formGroups.addDropDownFormGroup(name);
    };
    BaseForm.prototype.addFormGroup = function (formGroup) {
        return this.formGroups.addFormGroup(formGroup);
    };
    BaseForm.prototype.save = function (action) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var validationResult, errors_1, result, errors, model, ex_1, caption, error;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        validationResult = this.validate();
                        if (validationResult.hasErrors()) {
                            errors_1 = validationResult.values();
                            this.modalError.show(errors_1, "Unable to " + action.friendlyName);
                            return [2 /*return*/, new FormSaveResult_1.FormSaveResult(null, errors_1)];
                        }
                        errors = [];
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, 3, , 4]);
                        model = this.export();
                        return [4 /*yield*/, action.execute(model, { preventDefault: true })];
                    case 2:
                        result = _a.sent();
                        return [3 /*break*/, 4];
                    case 3:
                        ex_1 = _a.sent();
                        caption = '';
                        if (ex_1 instanceof AppApiError_1.AppApiError) {
                            errors.push.apply(errors, ex_1.getErrors());
                            caption = ex_1.getCaption();
                        }
                        else {
                            error = new ErrorModel_1.ErrorModel(ex_1.message, '', '');
                            errors.push(error);
                            new ConsoleLog_1.ConsoleLog().error(ex_1.message);
                        }
                        this.modalError.show(errors, caption);
                        return [3 /*break*/, 4];
                    case 4: return [2 /*return*/, new FormSaveResult_1.FormSaveResult(result, errors)];
                }
            });
        });
    };
    BaseForm.prototype.validate = function () {
        var errors = new ErrorList_1.ErrorList();
        this.formGroups.validate(errors);
        return errors;
    };
    BaseForm.prototype.import = function (values) {
        this.formGroups.import(values);
    };
    BaseForm.prototype.export = function () {
        var values = {};
        this.formGroups.export(values);
        return values;
    };
    return BaseForm;
}(FormComponent_1.FormComponent));
exports.BaseForm = BaseForm;
//# sourceMappingURL=BaseForm.js.map