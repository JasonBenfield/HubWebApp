"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthenticatorsGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var AuthenticatorsGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AuthenticatorsGroup, _super);
    function AuthenticatorsGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Authenticators') || this;
        _this.RegisterAuthenticatorAction = _this.createAction('RegisterAuthenticator', 'Register Authenticator');
        _this.RegisterUserAuthenticatorAction = _this.createAction('RegisterUserAuthenticator', 'Register User Authenticator');
        return _this;
    }
    AuthenticatorsGroup.prototype.RegisterAuthenticator = function (errorOptions) {
        return this.RegisterAuthenticatorAction.execute({}, errorOptions || {});
    };
    AuthenticatorsGroup.prototype.RegisterUserAuthenticator = function (model, errorOptions) {
        return this.RegisterUserAuthenticatorAction.execute(model, errorOptions || {});
    };
    return AuthenticatorsGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AuthenticatorsGroup = AuthenticatorsGroup;
//# sourceMappingURL=AuthenticatorsGroup.js.map