"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.AuthGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("../../Hub/AppApiGroup");
var AuthGroup = /** @class */ (function (_super) {
    tslib_1.__extends(AuthGroup, _super);
    function AuthGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'Auth') || this;
        _this.Index = _this.createView('Index');
        _this.Start = _this.createView('Start');
        _this.LoginAction = _this.createAction('Login', 'Login');
        _this.AuthenticateAction = _this.createAction('Authenticate', 'Authenticate');
        return _this;
    }
    AuthGroup.prototype.Login = function (model, errorOptions) {
        return this.LoginAction.execute(model, errorOptions || {});
    };
    AuthGroup.prototype.Authenticate = function (model, errorOptions) {
        return this.AuthenticateAction.execute(model, errorOptions || {});
    };
    return AuthGroup;
}(AppApiGroup_1.AppApiGroup));
exports.AuthGroup = AuthGroup;
//# sourceMappingURL=AuthGroup.js.map