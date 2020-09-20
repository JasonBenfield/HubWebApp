"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppApi = void 0;
var AppResourceUrl_1 = require("./AppResourceUrl");
var UserGroup_1 = require("./UserGroup");
var AppApi = /** @class */ (function () {
    function AppApi(events, baseUrl, app) {
        this.events = events;
        this.groups = {};
        this.resourceUrl = AppResourceUrl_1.AppResourceUrl.app(baseUrl, app, pageContext.CacheBust);
        this.addGroup(function (evts, ru) { return new UserGroup_1.UserGroup(evts, ru); });
    }
    Object.defineProperty(AppApi.prototype, "name", {
        get: function () { return this.resourceUrl.resourceName.app; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(AppApi.prototype, "url", {
        get: function () { return this.resourceUrl.relativeUrl; },
        enumerable: false,
        configurable: true
    });
    AppApi.prototype.addGroup = function (createGroup) {
        var group = createGroup(this.events, this.resourceUrl);
        this.groups[group.name] = group;
        return group;
    };
    AppApi.prototype.toString = function () {
        return "AppApi " + this.resourceUrl;
    };
    return AppApi;
}());
exports.AppApi = AppApi;
//# sourceMappingURL=AppApi.js.map