"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Apis = void 0;
var AppApiFactory_1 = require("@jasonbenfield/sharedwebapp/Api/AppApiFactory");
var ModalErrorComponent_1 = require("@jasonbenfield/sharedwebapp/Error/ModalErrorComponent");
var HubAppApi_1 = require("./Api/HubAppApi");
var Apis = /** @class */ (function () {
    function Apis(modalError) {
        this.modalError = new ModalErrorComponent_1.ModalErrorComponent(modalError);
    }
    Apis.prototype.hub = function () {
        var apiFactory = new AppApiFactory_1.AppApiFactory(this.modalError);
        return apiFactory.api(HubAppApi_1.HubAppApi);
    };
    return Apis;
}());
exports.Apis = Apis;
//# sourceMappingURL=Apis.js.map