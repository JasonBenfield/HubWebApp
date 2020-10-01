"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppResourceUrl = void 0;
var XtiPath_1 = require("./XtiPath");
var UrlBuilder_1 = require("./UrlBuilder");
var AppResourceUrl = /** @class */ (function () {
    function AppResourceUrl(baseUrl, resourceName, cacheBust) {
        this.baseUrl = baseUrl;
        this.resourceName = resourceName;
        this.cacheBust = cacheBust;
        this.url = new UrlBuilder_1.UrlBuilder(baseUrl)
            .addPart(resourceName.format());
        this.url.addQuery('cacheBust', cacheBust);
    }
    AppResourceUrl.app = function (baseUrl, appKey, version, cacheBust) {
        return new AppResourceUrl(baseUrl, XtiPath_1.XtiPath.app(appKey, version), cacheBust);
    };
    Object.defineProperty(AppResourceUrl.prototype, "relativeUrl", {
        get: function () {
            return new UrlBuilder_1.UrlBuilder("/" + this.resourceName.format());
        },
        enumerable: false,
        configurable: true
    });
    AppResourceUrl.prototype.withGroup = function (group) {
        return new AppResourceUrl(this.baseUrl, this.resourceName.withGroup(group), this.cacheBust);
    };
    AppResourceUrl.prototype.withAction = function (action) {
        return new AppResourceUrl(this.baseUrl, this.resourceName.withAction(action), this.cacheBust);
    };
    AppResourceUrl.prototype.toString = function () {
        return this.url.getUrl();
    };
    return AppResourceUrl;
}());
exports.AppResourceUrl = AppResourceUrl;
//# sourceMappingURL=AppResourceUrl.js.map