"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AppApiEvents_1 = require("./AppApiEvents");
var ConsoleLog_1 = require("./ConsoleLog");
var HostEnvironment_1 = require("./HostEnvironment");
var AppApiFactory = /** @class */ (function () {
    function AppApiFactory() {
    }
    Object.defineProperty(AppApiFactory.prototype, "defaultApiType", {
        set: function (defaultApi) {
            this._defaultApiType = defaultApi;
        },
        enumerable: true,
        configurable: true
    });
    AppApiFactory.prototype.defaultApi = function (modalError) {
        return this.api(this._defaultApiType, modalError);
    };
    AppApiFactory.prototype.api = function (apiCtor, modalError) {
        var api;
        var events = new AppApiEvents_1.AppApiEvents(function (err) {
            new ConsoleLog_1.ConsoleLog().error(err.toString());
            modalError.show(err.getErrors(), err.getCaption());
        });
        if (apiCtor === this._defaultApiType) {
            api = new apiCtor(events, location.protocol + "//" + location.host, 'Current');
        }
        else {
            var hostEnvironment = new HostEnvironment_1.HostEnvironment();
            api = new apiCtor(events, pageContext.BaseUrl, hostEnvironment.isProduction ? '' : 'Current');
        }
        return api;
    };
    return AppApiFactory;
}());
exports.AppApiFactory = AppApiFactory;
//# sourceMappingURL=AppApiFactory.js.map