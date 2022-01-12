"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EditUserFormView = void 0;
var tslib_1 = require("tslib");
// Generated code
var BaseFormView_1 = require("@jasonbenfield/sharedwebapp/Forms/BaseFormView");
var EditUserFormView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EditUserFormView, _super);
    function EditUserFormView() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.UserID = _this.addInputFormGroup();
        _this.PersonName = _this.addInputFormGroup();
        _this.Email = _this.addInputFormGroup();
        return _this;
    }
    return EditUserFormView;
}(BaseFormView_1.BaseFormView));
exports.EditUserFormView = EditUserFormView;
//# sourceMappingURL=EditUserFormView.js.map