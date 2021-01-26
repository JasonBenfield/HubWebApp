"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var _ = require("lodash");
var DebouncedAction = /** @class */ (function () {
    function DebouncedAction(func, wait) {
        this.debounced = _.debounce(func, wait);
    }
    DebouncedAction.prototype.execute = function () {
        var args = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            args[_i] = arguments[_i];
        }
        return this.debounced(args);
    };
    return DebouncedAction;
}());
exports.DebouncedAction = DebouncedAction;
//# sourceMappingURL=DebouncedAction.js.map