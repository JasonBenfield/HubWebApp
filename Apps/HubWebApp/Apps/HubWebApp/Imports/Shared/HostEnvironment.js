"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.HostEnvironment = void 0;
var HostEnvironment = /** @class */ (function () {
    function HostEnvironment() {
        this.isTest = pageContext.EnvironmentName === 'Test';
        this.isDevelopment = pageContext.EnvironmentName === 'Development';
        this.isStaging = pageContext.EnvironmentName === 'Staging';
        this.isProduction = pageContext.EnvironmentName === 'Production';
    }
    return HostEnvironment;
}());
exports.HostEnvironment = HostEnvironment;
//# sourceMappingURL=HostEnvironment.js.map