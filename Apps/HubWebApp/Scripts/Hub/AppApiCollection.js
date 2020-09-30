"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.setApi = exports.hub = exports.api = exports.AppApiCollection = void 0;
var tslib_1 = require("tslib");
var HubAppApi_1 = require("./Api/HubAppApi");
var BaseAppApiCollection_1 = require("./BaseAppApiCollection");
var AppApiCollection = /** @class */ (function (_super) {
    tslib_1.__extends(AppApiCollection, _super);
    function AppApiCollection(events) {
        var _this = _super.call(this, events) || this;
        _this.Hub = _this.addThisApp(function (evts) { return new HubAppApi_1.HubAppApi(evts, location.protocol + "//" + location.host, 'Current'); });
        return _this;
    }
    return AppApiCollection;
}(BaseAppApiCollection_1.BaseAppApiCollection));
exports.AppApiCollection = AppApiCollection;
function api() {
    return BaseAppApiCollection_1.baseApi;
}
exports.api = api;
function hub() {
    return api().Hub;
}
exports.hub = hub;
function setApi(_api) {
    BaseAppApiCollection_1.setBaseApi(_api);
}
exports.setApi = setApi;
//# sourceMappingURL=AppApiCollection.js.map