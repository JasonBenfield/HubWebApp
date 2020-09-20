"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var UserPageViewModel_1 = require("./UserPageViewModel");
var Alert_1 = require("../Alert");
var UrlBuilder_1 = require("../UrlBuilder");
var cpwstart_1 = require("cpwstart");
var WebPage_1 = require("../WebPage");
var BaseAppApiCollection_1 = require("../BaseAppApiCollection");
var UserPage = /** @class */ (function () {
    function UserPage(vm) {
        this.vm = vm;
        this.alert = new Alert_1.Alert(this.vm.alert);
        this.goToReturnUrl();
    }
    UserPage.prototype.goToReturnUrl = function () {
        this.alert.info('Opening Page...');
        var urlBuilder = UrlBuilder_1.UrlBuilder.current();
        var returnUrl = urlBuilder.getQueryValue('returnUrl');
        if (returnUrl) {
            returnUrl = decodeURIComponent(returnUrl);
        }
        returnUrl = BaseAppApiCollection_1.baseApi.thisApp.url.addPart(returnUrl).getUrl();
        new WebPage_1.WebPage(returnUrl).open();
    };
    return UserPage;
}());
cpwstart_1.startup(function () { return new UserPageViewModel_1.UserPageViewModel(); }, function (vm) { return new UserPage(vm); });
//# sourceMappingURL=UserPage.js.map