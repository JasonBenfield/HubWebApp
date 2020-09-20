"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.setBaseApi = exports.baseApi = exports.BaseAppApiCollection = void 0;
var BaseAppApiCollection = /** @class */ (function () {
    function BaseAppApiCollection(events) {
        this.events = events;
        this.apps = {};
    }
    BaseAppApiCollection.prototype.addThisApp = function (createApp) {
        var app = this.addApp(createApp);
        this._thisApp = app;
        return app;
    };
    BaseAppApiCollection.prototype.addApp = function (createApp) {
        var app = createApp(this.events);
        this.apps[app.name] = app;
        return app;
    };
    Object.defineProperty(BaseAppApiCollection.prototype, "thisApp", {
        get: function () { return this._thisApp; },
        enumerable: false,
        configurable: true
    });
    return BaseAppApiCollection;
}());
exports.BaseAppApiCollection = BaseAppApiCollection;
function setBaseApi(_api) {
    exports.baseApi = _api;
}
exports.setBaseApi = setBaseApi;
//# sourceMappingURL=BaseAppApiCollection.js.map