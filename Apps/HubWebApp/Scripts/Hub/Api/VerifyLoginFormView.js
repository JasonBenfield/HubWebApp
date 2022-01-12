"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.VerifyLoginFormView = void 0;
var tslib_1 = require("tslib");
// Generated code
var BaseFormView_1 = require("@jasonbenfield/sharedwebapp/Forms/BaseFormView");
var VerifyLoginFormView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(VerifyLoginFormView, _super);
    function VerifyLoginFormView() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.UserName = _this.addInputFormGroup();
        _this.Password = _this.addInputFormGroup();
        return _this;
    }
    return VerifyLoginFormView;
}(BaseFormView_1.BaseFormView));
exports.VerifyLoginFormView = VerifyLoginFormView;
//# sourceMappingURL=VerifyLoginFormView.js.map