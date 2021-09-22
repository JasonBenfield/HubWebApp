"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DelayedAction = void 0;
var tslib_1 = require("tslib");
var DelayedAction = /** @class */ (function () {
    function DelayedAction(func, wait) {
        this.func = func;
        this.wait = wait;
    }
    DelayedAction.delay = function (wait) {
        return new DelayedAction(function () { }, wait).execute();
    };
    DelayedAction.prototype.execute = function () {
        var _this = this;
        var args = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            args[_i] = arguments[_i];
        }
        var result;
        return new Promise(function (resolve) {
            setTimeout(function () {
                var _a;
                result = (_a = _this.func).call.apply(_a, tslib_1.__spreadArray([_this.func], args));
                resolve(result);
            }, _this.wait);
        });
    };
    return DelayedAction;
}());
exports.DelayedAction = DelayedAction;
//# sourceMappingURL=DelayedAction.js.map