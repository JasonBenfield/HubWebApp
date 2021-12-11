"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserForm = void 0;
var tslib_1 = require("tslib");
// Generated code
var BaseForm_1 = require("XtiShared/Forms/BaseForm");
var FormComponentViewModel_1 = require("XtiShared/Html/FormComponentViewModel");
var EditUserForm = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EditUserForm, _super);
    function EditUserForm(vm) {
        if (vm === void 0) { vm = new FormComponentViewModel_1.FormComponentViewModel(); }
        var _this = _super.call(this, 'EditUserForm', vm) || this;
        _this.UserID = _this.addHiddenNumberFormGroup('UserID');
        _this.PersonName = _this.addTextInputFormGroup('PersonName');
        _this.Email = _this.addTextInputFormGroup('Email');
        _this.UserID.setCaption('User ID');
        _this.UserID.constraints.mustNotBeNull();
        _this.PersonName.setCaption('Person Name');
        _this.Email.setCaption('Email');
        return _this;
    }
    return EditUserForm;
}(BaseForm_1.BaseForm));
exports.EditUserForm = EditUserForm;
//# sourceMappingURL=EditUserForm.js.map