"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.startup = void 0;
var PageLoader_1 = require("../Shared/PageLoader");
var AppApiEvents_1 = require("../Shared/AppApiEvents");
var ConsoleLog_1 = require("../Shared/ConsoleLog");
var ModalErrorComponent_1 = require("../Shared/Error/ModalErrorComponent");
var tsyringe_1 = require("tsyringe");
var HubAppApi_1 = require("./Api/HubAppApi");
var AuthenticatorAppApi_1 = require("../Authenticator/Api/AuthenticatorAppApi");
var AppApi_1 = require("../Shared/AppApi");
var HostEnvironment_1 = require("../Shared/HostEnvironment");
var LogoutUrl_1 = require("../Authenticator/LogoutUrl");
function startup(pageVM, page) {
    tsyringe_1.container.register('PageVM', { useFactory: function (c) { return c.resolve(pageVM); } });
    tsyringe_1.container.register('Page', { useFactory: function (c) { return c.resolve(page); } });
    tsyringe_1.container.register(AppApiEvents_1.AppApiEvents, {
        useFactory: function (c) { return new AppApiEvents_1.AppApiEvents(function (err) {
            new ConsoleLog_1.ConsoleLog().error(err.toString());
            c.resolve(ModalErrorComponent_1.ModalErrorComponent).show(err.getErrors(), err.getCaption());
        }); }
    });
    var hostEnvironment = new HostEnvironment_1.HostEnvironment();
    tsyringe_1.container.register(AuthenticatorAppApi_1.AuthenticatorAppApi, {
        useFactory: function (c) { return new AuthenticatorAppApi_1.AuthenticatorAppApi(c.resolve(AppApiEvents_1.AppApiEvents), pageContext.BaseUrl, hostEnvironment.isProduction ? '' : 'Current'); }
    });
    tsyringe_1.container.register(HubAppApi_1.HubAppApi, {
        useFactory: function (c) { return new HubAppApi_1.HubAppApi(c.resolve(AppApiEvents_1.AppApiEvents), location.protocol + "//" + location.host, 'Current'); }
    });
    tsyringe_1.container.register(AppApi_1.AppApi, { useFactory: function (c) { return c.resolve(HubAppApi_1.HubAppApi); } });
    tsyringe_1.container.register('LogoutUrl', {
        useToken: LogoutUrl_1.LogoutUrl
    });
    new PageLoader_1.PageLoader().load();
}
exports.startup = startup;
//# sourceMappingURL=Startup.js.map