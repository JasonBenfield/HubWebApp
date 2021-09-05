"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserPage = void 0;
var UrlBuilder_1 = require("../UrlBuilder");
var xtistart_1 = require("xtistart");
var WebPage_1 = require("../WebPage");
var MessageAlert_1 = require("../MessageAlert");
var UserPage = /** @class */ (function () {
    function UserPage(page) {
        this.page = page;
        this.alert = this.page.addContent(new MessageAlert_1.MessageAlert());
        this.goToReturnUrl();
    }
    UserPage.prototype.goToReturnUrl = function () {
        this.alert.info('Opening Page...');
        var urlBuilder = UrlBuilder_1.UrlBuilder.current();
        var returnUrl = urlBuilder.getQueryValue('returnUrl');
        if (returnUrl) {
            returnUrl = decodeURIComponent(returnUrl);
        }
        var defaultApi = this.page.defaultApi();
        returnUrl = defaultApi ? defaultApi.url.addPart(returnUrl).value() : '/';
        new WebPage_1.WebPage(returnUrl).open();
    };
    return UserPage;
}());
exports.UserPage = UserPage;
new UserPage(new xtistart_1.Startup().build());
//# sourceMappingURL=UserPage.js.map