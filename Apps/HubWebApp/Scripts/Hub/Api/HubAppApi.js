"use strict";
// Generated code
Object.defineProperty(exports, "__esModule", { value: true });
exports.HubAppApi = void 0;
var tslib_1 = require("tslib");
var AppApi_1 = require("XtiShared/AppApi");
var UserGroup_1 = require("./UserGroup");
var UserAdminGroup_1 = require("./UserAdminGroup");
var AppsGroup_1 = require("./AppsGroup");
var AppGroup_1 = require("./AppGroup");
var ResourceGroupGroup_1 = require("./ResourceGroupGroup");
var ResourceGroup_1 = require("./ResourceGroup");
var ModCategoryGroup_1 = require("./ModCategoryGroup");
var UsersGroup_1 = require("./UsersGroup");
var UserInquiryGroup_1 = require("./UserInquiryGroup");
var UserMaintenanceGroup_1 = require("./UserMaintenanceGroup");
var HubAppApi = /** @class */ (function (_super) {
    tslib_1.__extends(HubAppApi, _super);
    function HubAppApi(events, baseUrl, version) {
        if (version === void 0) { version = ''; }
        var _this = _super.call(this, events, baseUrl, 'Hub', version || HubAppApi.DefaultVersion) || this;
        _this.User = _this.addGroup(function (evts, resourceUrl) { return new UserGroup_1.UserGroup(evts, resourceUrl); });
        _this.UserAdmin = _this.addGroup(function (evts, resourceUrl) { return new UserAdminGroup_1.UserAdminGroup(evts, resourceUrl); });
        _this.Apps = _this.addGroup(function (evts, resourceUrl) { return new AppsGroup_1.AppsGroup(evts, resourceUrl); });
        _this.App = _this.addGroup(function (evts, resourceUrl) { return new AppGroup_1.AppGroup(evts, resourceUrl); });
        _this.ResourceGroup = _this.addGroup(function (evts, resourceUrl) { return new ResourceGroupGroup_1.ResourceGroupGroup(evts, resourceUrl); });
        _this.Resource = _this.addGroup(function (evts, resourceUrl) { return new ResourceGroup_1.ResourceGroup(evts, resourceUrl); });
        _this.ModCategory = _this.addGroup(function (evts, resourceUrl) { return new ModCategoryGroup_1.ModCategoryGroup(evts, resourceUrl); });
        _this.Users = _this.addGroup(function (evts, resourceUrl) { return new UsersGroup_1.UsersGroup(evts, resourceUrl); });
        _this.UserInquiry = _this.addGroup(function (evts, resourceUrl) { return new UserInquiryGroup_1.UserInquiryGroup(evts, resourceUrl); });
        _this.UserMaintenance = _this.addGroup(function (evts, resourceUrl) { return new UserMaintenanceGroup_1.UserMaintenanceGroup(evts, resourceUrl); });
        return _this;
    }
    HubAppApi.DefaultVersion = 'V21';
    return HubAppApi;
}(AppApi_1.AppApi));
exports.HubAppApi = HubAppApi;
//# sourceMappingURL=HubAppApi.js.map