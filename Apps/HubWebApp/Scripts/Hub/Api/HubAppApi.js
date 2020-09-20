"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.HubAppApi = void 0;
var tslib_1 = require("tslib");
var AppApi_1 = require("../../Hub/AppApi");
var AuthGroup_1 = require("./AuthGroup");
var UserAdminGroup_1 = require("./UserAdminGroup");
var RestrictedGroup_1 = require("./RestrictedGroup");
var HubAppApi = /** @class */ (function (_super) {
    tslib_1.__extends(HubAppApi, _super);
    function HubAppApi(events, baseUrl) {
        var _this = _super.call(this, events, baseUrl, 'Hub') || this;
        _this.Auth = _this.addGroup(function (evts, resourceUrl) { return new AuthGroup_1.AuthGroup(evts, resourceUrl); });
        _this.UserAdmin = _this.addGroup(function (evts, resourceUrl) { return new UserAdminGroup_1.UserAdminGroup(evts, resourceUrl); });
        _this.Restricted = _this.addGroup(function (evts, resourceUrl) { return new RestrictedGroup_1.RestrictedGroup(evts, resourceUrl); });
        return _this;
    }
    return HubAppApi;
}(AppApi_1.AppApi));
exports.HubAppApi = HubAppApi;
//# sourceMappingURL=HubAppApi.js.map