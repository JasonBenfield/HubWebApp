"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserInquiryGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var UserInquiryGroup = /** @class */ (function (_super) {
    tslib_1.__extends(UserInquiryGroup, _super);
    function UserInquiryGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'UserInquiry') || this;
        _this.GetUserAction = _this.createAction('GetUser', 'Get User');
        _this.GetUserByUserNameAction = _this.createAction('GetUserByUserName', 'Get User By User Name');
        _this.GetCurrentUserAction = _this.createAction('GetCurrentUser', 'Get Current User');
        _this.RedirectToAppUser = _this.createView('RedirectToAppUser');
        return _this;
    }
    UserInquiryGroup.prototype.GetUser = function (model, errorOptions) {
        return this.GetUserAction.execute(model, errorOptions || {});
    };
    UserInquiryGroup.prototype.GetUserByUserName = function (model, errorOptions) {
        return this.GetUserByUserNameAction.execute(model, errorOptions || {});
    };
    UserInquiryGroup.prototype.GetCurrentUser = function (errorOptions) {
        return this.GetCurrentUserAction.execute({}, errorOptions || {});
    };
    return UserInquiryGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UserInquiryGroup = UserInquiryGroup;
//# sourceMappingURL=UserInquiryGroup.js.map