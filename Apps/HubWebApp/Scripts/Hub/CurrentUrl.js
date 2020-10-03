"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CurrentUrl = void 0;
var XtiPath_1 = require("./XtiPath");
var CurrentUrl = /** @class */ (function () {
    function CurrentUrl() {
        this.baseUrl = location.protocol + "//" + location.host;
        var split = location.pathname.split('/');
        this.path = new XtiPath_1.XtiPath(split[0], split[1], split[2], split[3], split[4]);
    }
    return CurrentUrl;
}());
exports.CurrentUrl = CurrentUrl;
//# sourceMappingURL=CurrentUrl.js.map