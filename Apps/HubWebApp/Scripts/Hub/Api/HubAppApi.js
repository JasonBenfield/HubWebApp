"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.HubAppApi = void 0;
var tslib_1 = require("tslib");
var AppApi_1 = require("XtiShared/AppApi");
var UserGroup_1 = require("./UserGroup");
var UserCacheGroup_1 = require("./UserCacheGroup");
var AuthGroup_1 = require("./AuthGroup");
var AuthApiGroup_1 = require("./AuthApiGroup");
var PermanentLogGroup_1 = require("./PermanentLogGroup");
var AppsGroup_1 = require("./AppsGroup");
var AppGroup_1 = require("./AppGroup");
var ResourceGroupGroup_1 = require("./ResourceGroupGroup");
var ResourceGroup_1 = require("./ResourceGroup");
var ModCategoryGroup_1 = require("./ModCategoryGroup");
var UsersGroup_1 = require("./UsersGroup");
var UserInquiryGroup_1 = require("./UserInquiryGroup");
var UserMaintenanceGroup_1 = require("./UserMaintenanceGroup");
var AppUserGroup_1 = require("./AppUserGroup");
var AppUserMaintenanceGroup_1 = require("./AppUserMaintenanceGroup");
var HubAppApi = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(HubAppApi, _super);
    function HubAppApi(events, baseUrl, version) {
        if (version === void 0) { version = ''; }
        var _this = _super.call(this, events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion) || this;
        _this.User = _this.addGroup(function (evts, resourceUrl) { return new UserGroup_1.UserGroup(evts, resourceUrl); });
        _this.UserCache = _this.addGroup(function (evts, resourceUrl) { return new UserCacheGroup_1.UserCacheGroup(evts, resourceUrl); });
        _this.Auth = _this.addGroup(function (evts, resourceUrl) { return new AuthGroup_1.AuthGroup(evts, resourceUrl); });
        _this.AuthApi = _this.addGroup(function (evts, resourceUrl) { return new AuthApiGroup_1.AuthApiGroup(evts, resourceUrl); });
        _this.PermanentLog = _this.addGroup(function (evts, resourceUrl) { return new PermanentLogGroup_1.PermanentLogGroup(evts, resourceUrl); });
        _this.Apps = _this.addGroup(function (evts, resourceUrl) { return new AppsGroup_1.AppsGroup(evts, resourceUrl); });
        _this.App = _this.addGroup(function (evts, resourceUrl) { return new AppGroup_1.AppGroup(evts, resourceUrl); });
        _this.ResourceGroup = _this.addGroup(function (evts, resourceUrl) { return new ResourceGroupGroup_1.ResourceGroupGroup(evts, resourceUrl); });
        _this.Resource = _this.addGroup(function (evts, resourceUrl) { return new ResourceGroup_1.ResourceGroup(evts, resourceUrl); });
        _this.ModCategory = _this.addGroup(function (evts, resourceUrl) { return new ModCategoryGroup_1.ModCategoryGroup(evts, resourceUrl); });
        _this.Users = _this.addGroup(function (evts, resourceUrl) { return new UsersGroup_1.UsersGroup(evts, resourceUrl); });
        _this.UserInquiry = _this.addGroup(function (evts, resourceUrl) { return new UserInquiryGroup_1.UserInquiryGroup(evts, resourceUrl); });
        _this.UserMaintenance = _this.addGroup(function (evts, resourceUrl) { return new UserMaintenanceGroup_1.UserMaintenanceGroup(evts, resourceUrl); });
        _this.AppUser = _this.addGroup(function (evts, resourceUrl) { return new AppUserGroup_1.AppUserGroup(evts, resourceUrl); });
        _this.AppUserMaintenance = _this.addGroup(function (evts, resourceUrl) { return new AppUserMaintenanceGroup_1.AppUserMaintenanceGroup(evts, resourceUrl); });
        return _this;
    }
    HubAppApi.DefaultVersion = 'V21';
    return HubAppApi;
}(AppApi_1.AppApi));
exports.HubAppApi = HubAppApi;
//# sourceMappingURL=HubAppApi.js.map