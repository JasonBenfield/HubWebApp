"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Startup = void 0;
var tslib_1 = require("tslib");
var HubAppApi_1 = require("../Hub/Api/HubAppApi");
var BaseStartup_1 = require("XtiShared/BaseStartup");
var Startup = /** @class */ (function (_super) {
    tslib_1.__extends(Startup, _super);
    function Startup() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    Startup.prototype.getDefaultApi = function () {
        return HubAppApi_1.HubAppApi;
    };
    return Startup;
}(BaseStartup_1.BaseStartup));
exports.Startup = Startup;
//# sourceMappingURL=Startup.js.map