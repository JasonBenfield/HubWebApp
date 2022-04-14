"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.ExternalAuthGroup = void 0;
var tslib_1 = require("tslib");
var AppApiGroup_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiGroup");
var ExternalAuthGroup = /** @class */ (function (_super) {
    tslib_1.__extends(ExternalAuthGroup, _super);
    function ExternalAuthGroup(events, resourceUrl) {
        var _this = _super.call(this, events, resourceUrl, 'ExternalAuth') || this;
        _this.Login = _this.createView('Login');
        return _this;
    }
    return ExternalAuthGroup;
}(AppApiGroup_1.AppApiGroup));
exports.ExternalAuthGroup = ExternalAuthGroup;
//# sourceMappingURL=ExternalAuthGroup.js.map