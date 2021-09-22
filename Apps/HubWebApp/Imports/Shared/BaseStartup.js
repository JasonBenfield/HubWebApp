"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BaseStartup = exports.defaultApi = void 0;
var PageFrame_1 = require("./PageFrame");
var BaseStartup = /** @class */ (function () {
    function BaseStartup() {
    }
    BaseStartup.prototype.build = function () {
        var pageFrame = new PageFrame_1.PageFrame();
        pageFrame.setDefaultApiType(this.getDefaultApi());
        pageFrame.load();
        return pageFrame;
    };
    return BaseStartup;
}());
exports.BaseStartup = BaseStartup;
//# sourceMappingURL=BaseStartup.js.map