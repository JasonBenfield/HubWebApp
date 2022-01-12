"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserForm = void 0;
var tslib_1 = require("tslib");
// Generated code
var BaseForm_1 = require("@jasonbenfield/sharedwebapp/Forms/BaseForm");
var EditUserForm = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EditUserForm, _super);
    function EditUserForm(view) {
        var _this = _super.call(this, 'EditUserForm', view) || this;
        _this.UserID = _this.addHiddenNumberFormGroup('UserID', _this.view.UserID);
        _this.PersonName = _this.addTextInputFormGroup('PersonName', _this.view.PersonName);
        _this.Email = _this.addTextInputFormGroup('Email', _this.view.Email);
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