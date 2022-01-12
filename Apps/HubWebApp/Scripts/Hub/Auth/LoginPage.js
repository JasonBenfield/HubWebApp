"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppApiFactory_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiFactory");
var ModalErrorComponent_1 = require("@jasonbenfield/sharedwebapp/Error/ModalErrorComponent");
var Startup_1 = require("@jasonbenfield/sharedwebapp/Startup");
var HubAppApi_1 = require("../Api/HubAppApi");
var LoginComponent_1 = require("./LoginComponent");
var LoginPageView_1 = require("./LoginPageView");
var LoginPage = /** @class */ (function () {
    function LoginPage(page) {
        var view = new LoginPageView_1.LoginPageView(page);
        var apiFactory = new AppApiFactory_1.AppApiFactory(new ModalErrorComponent_1.ModalErrorComponent(page.modalError));
        var hubApi = apiFactory.api(HubAppApi_1.HubAppApi);
        new LoginComponent_1.LoginComponent(hubApi, view.loginComponent);
    }
    return LoginPage;
}());
new LoginPage(new Startup_1.Startup().build());
//# sourceMappingURL=LoginPage.js.map