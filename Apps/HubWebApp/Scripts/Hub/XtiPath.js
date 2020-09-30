"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.XtiPath = void 0;
var JoinedStrings_1 = require("./JoinedStrings");
var XtiPath = /** @class */ (function () {
    function XtiPath(app, version, group, action) {
        this.app = app;
        this.version = version;
        this.group = group;
        this.action = action;
        var parts = [this.app, this.version];
        if (this.group) {
            parts.push(this.group);
            if (this.action) {
                parts.push(this.action);
            }
        }
        this.value = new JoinedStrings_1.JoinedStrings('/', parts).value();
    }
    XtiPath.app = function (appKey, version) { return new XtiPath(appKey, version, null, null); };
    XtiPath.prototype.withGroup = function (group) {
        return new XtiPath(this.app, this.version, group, null);
    };
    XtiPath.prototype.withAction = function (action) {
        return new XtiPath(this.app, this.version, this.group, action);
    };
    XtiPath.prototype.format = function () {
        return this.value;
    };
    XtiPath.prototype.equals = function (other) {
        if (other) {
            return this.value === other.value;
        }
        return false;
    };
    XtiPath.prototype.toString = function () {
        return this.value;
    };
    return XtiPath;
}());
exports.XtiPath = XtiPath;
//# sourceMappingURL=XtiPath.js.map