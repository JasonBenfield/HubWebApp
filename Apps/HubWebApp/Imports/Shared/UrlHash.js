"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var NamedValue_1 = require("./NamedValue");
var _ = require("lodash");
var JoinedStrings_1 = require("./JoinedStrings");
var UrlHash = /** @class */ (function () {
    function UrlHash(hash) {
        this.hashValues = [];
        if (hash) {
            if (typeof hash === 'string') {
                this.pushHashValues(hash);
            }
            else {
                this.hashValues = hash;
            }
        }
    }
    UrlHash.prototype.pushHashValues = function (query) {
        var _this = this;
        var parts = query.split('&');
        _(parts).forEach(function (part) {
            var nameValue = part.split('=');
            var name = nameValue[0];
            var value = '';
            if (nameValue[1]) {
                value = nameValue[1];
            }
            _this.hashValues.push(new NamedValue_1.NamedValue(name, value));
        });
    };
    UrlHash.prototype.getValues = function () {
        return this.hashValues;
    };
    UrlHash.prototype.getValue = function (name) {
        var hashValue = _(this.hashValues).find(function (qv) { return qv.name === name; });
        return hashValue ? hashValue.value : null;
    };
    UrlHash.prototype.clear = function () {
        this.hashValues.splice(0, this.hashValues.length);
        return this;
    };
    UrlHash.prototype.hasQuery = function (name) {
        var queryValue = _(this.hashValues).find(function (qv) { return qv.name === name; });
        return Boolean(queryValue);
    };
    UrlHash.prototype.toString = function () {
        var str = '';
        if (this.hashValues.length > 0) {
            str = new JoinedStrings_1.JoinedStrings('&', this.hashValues).value();
        }
        return str;
    };
    return UrlHash;
}());
exports.UrlHash = UrlHash;
//# sourceMappingURL=UrlHash.js.map