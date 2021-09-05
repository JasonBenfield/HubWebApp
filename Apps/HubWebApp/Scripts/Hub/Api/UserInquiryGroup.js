"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserInquiryGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("XtiShared/AppApiGroup");
var UserInquiryGroup = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserInquiryGroup, _super);
    function UserInquiryGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'UserInquiry') || this;
        _this.GetUserAction = _this.createAction('GetUser', 'Get User');
        _this.RedirectToAppUser = _this.createView('RedirectToAppUser');
        return _this;
    }
    UserInquiryGroup.prototype.GetUser = function (model, errorOptions) {
        return this.GetUserAction.execute(model, errorOptions || {});
    };
    return UserInquiryGroup;
}(AppApiGroup_1.AppApiGroup));
exports.UserInquiryGroup = UserInquiryGroup;
//# sourceMappingURL=UserInquiryGroup.js.map