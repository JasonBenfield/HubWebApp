"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.VerifyLoginForm = void 0;
var tslib_1 = require("tslib");
// Generated code
var BaseForm_1 = require("XtiShared/Forms/BaseForm");
var FormComponentViewModel_1 = require("XtiShared/Html/FormComponentViewModel");
var VerifyLoginForm = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(VerifyLoginForm, _super);
    function VerifyLoginForm(vm) {
        if (vm === void 0) { vm = new FormComponentViewModel_1.FormComponentViewModel(); }
        var _this = _super.call(this, 'VerifyLoginForm', vm) || this;
        _this.UserName = _this.addTextInputFormGroup('UserName');
        _this.Password = _this.addTextInputFormGroup('Password');
        _this.UserName.setCaption('User Name');
        _this.UserName.constraints.mustNotBeNull();
        _this.UserName.constraints.mustNotBeWhitespace('Must not be blank');
        _this.UserName.setMaxLength(100);
        _this.Password.setCaption('Password');
        _this.Password.constraints.mustNotBeNull();
        _this.Password.constraints.mustNotBeWhitespace('Must not be blank');
        _this.Password.setMaxLength(100);
        _this.Password.protect();
        return _this;
    }
    return VerifyLoginForm;
}(BaseForm_1.BaseForm));
exports.VerifyLoginForm = VerifyLoginForm;
//# sourceMappingURL=VerifyLoginForm.js.map