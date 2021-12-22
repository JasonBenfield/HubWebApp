"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var Apis_1 = require("../Apis");
var LoginComponent_1 = require("./LoginComponent");
var LoginPageView_1 = require("./LoginPageView");
var LoginPage = /** @class */ (function () {
    function LoginPage(page) {
        var view = new LoginPageView_1.LoginPageView(page);
        var hubApi = new Apis_1.Apis(page.modalError).hub();
        new LoginComponent_1.LoginComponent(hubApi, view.loginComponent);
    }
    return LoginPage;
}());
new LoginPage(new Startup_1.Startup().build());
//# sourceMappingURL=LoginPage.js.map