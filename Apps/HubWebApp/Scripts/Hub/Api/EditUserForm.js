"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserForm = void 0;
var tslib_1 = require("tslib");
var Form_1 = require("XtiShared/Forms/Form");
var EditUserForm = /** @class */ (function (_super) {
    tslib_1.__extends(EditUserForm, _super);
    function EditUserForm(vm) {
        var _this = _super.call(this, 'EditUserForm') || this;
        _this.vm = vm;
        _this.UserID = _this.addHiddenNumberField('UserID', _this.vm.UserID);
        _this.PersonName = _this.addTextInputField('PersonName', _this.vm.PersonName);
        _this.Email = _this.addTextInputField('Email', _this.vm.Email);
        _this.UserID.setCaption('User ID');
        _this.UserID.constraints.mustNotBeNull();
        _this.PersonName.setCaption('Person Name');
        _this.Email.setCaption('Email');
        return _this;
    }
    return EditUserForm;
}(Form_1.Form));
exports.EditUserForm = EditUserForm;
//# sourceMappingURL=EditUserForm.js.map