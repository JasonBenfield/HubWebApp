"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.HubAppApi = void 0;
var tslib_1 = require("tslib");
var AppApi_1 = require("../../Shared/AppApi");
var UserGroup_1 = require("./UserGroup");
var UserAdminGroup_1 = require("./UserAdminGroup");
var HubAppApi = /** @class */ (function (_super) {
    tslib_1.__extends(HubAppApi, _super);
    function HubAppApi(events, baseUrl, version) {
        if (version === void 0) { version = ''; }
        var _this = _super.call(this, events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion) || this;
        _this.User = _this.addGroup(function (evts, resourceUrl) { return new UserGroup_1.UserGroup(evts, resourceUrl); });
        _this.UserAdmin = _this.addGroup(function (evts, resourceUrl) { return new UserAdminGroup_1.UserAdminGroup(evts, resourceUrl); });
        return _this;
    }
    HubAppApi.DefaultVersion = 'Current';
    return HubAppApi;
}(AppApi_1.AppApi));
exports.HubAppApi = HubAppApi;
//# sourceMappingURL=HubAppApi.js.map