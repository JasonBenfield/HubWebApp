"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppResourceName = void 0;
var JoinedStrings_1 = require("./JoinedStrings");
var AppResourceName = /** @class */ (function () {
    function AppResourceName(app, group, action) {
        this.app = app;
        this.group = group;
        this.action = action;
        var parts = [this.app];
        if (this.group) {
            parts.push(this.group);
            if (this.action) {
                parts.push(this.action);
            }
        }
        this.value = new JoinedStrings_1.JoinedStrings('/', parts).value();
    }
    AppResourceName.app = function (appKey) { return new AppResourceName(appKey, null, null); };
    AppResourceName.prototype.withGroup = function (group) {
        return new AppResourceName(this.app, group, null);
    };
    AppResourceName.prototype.withAction = function (action) {
        return new AppResourceName(this.app, this.group, action);
    };
    AppResourceName.prototype.format = function () {
        return this.value;
    };
    AppResourceName.prototype.equals = function (other) {
        if (other) {
            return this.value === other.value;
        }
        return false;
    };
    AppResourceName.prototype.toString = function () {
        return this.value;
    };
    return AppResourceName;
}());
exports.AppResourceName = AppResourceName;
//# sourceMappingURL=AppResourceName.js.map