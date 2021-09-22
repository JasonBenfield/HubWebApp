"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UrlHashBuilder = void 0;
var NamedValue_1 = require("./NamedValue");
var _ = require("lodash");
var UrlHash_1 = require("./UrlHash");
var UrlHashBuilder = /** @class */ (function () {
    function UrlHashBuilder(hash) {
        if (typeof hash === 'string') {
            this._hash = new UrlHash_1.UrlHash(hash);
        }
        else {
            this._hash = hash;
        }
    }
    Object.defineProperty(UrlHashBuilder.prototype, "hash", {
        get: function () { return this._hash; },
        enumerable: false,
        configurable: true
    });
    UrlHashBuilder.prototype.getValues = function () {
        return this._hash.getValues();
    };
    UrlHashBuilder.prototype.getValue = function (name) {
        return this._hash.getValue(name);
    };
    UrlHashBuilder.prototype.clear = function () {
        this._hash = new UrlHash_1.UrlHash('');
        return this;
    };
    UrlHashBuilder.prototype.hasQuery = function (name) {
        return this._hash.hasQuery(name);
    };
    UrlHashBuilder.prototype.removeQuery = function (name) {
        var hashValues = this._hash.getValues();
        for (var i = hashValues.length - 1; i >= 0; i--) {
            var queryPart = hashValues[i];
            if (queryPart.name === name) {
                hashValues.splice(i, 1);
            }
        }
        this._hash = new UrlHash_1.UrlHash(hashValues);
        return this;
    };
    UrlHashBuilder.prototype.replaceQuery = function (name, value) {
        this.removeQuery(name);
        return this.addQuery(name, value);
    };
    UrlHashBuilder.prototype.addQuery = function (name, value) {
        var _this = this;
        var hashValues = this._hash.getValues();
        if (name) {
            if (value instanceof Date) {
                var queryValue = value === undefined || value === null
                    ? null
                    : value.toISOString();
                hashValues.push(new NamedValue_1.NamedValue(name, queryValue));
            }
            else if (typeof value === 'string') {
                var queryValue = void 0;
                if (value !== undefined && value !== null) {
                    queryValue = value;
                }
                hashValues.push(new NamedValue_1.NamedValue(name, queryValue));
            }
            else if (typeof value === 'number') {
                var queryValue = void 0;
                if (value !== undefined && value !== null) {
                    queryValue = value.toString();
                }
                hashValues.push(new NamedValue_1.NamedValue(name, queryValue));
            }
            else if (_.isArray(value)) {
                _(value).forEach(function (arrValue) {
                    _this.addQuery(name, arrValue);
                });
            }
            else {
                hashValues.push(new NamedValue_1.NamedValue(name, value && value.toString()));
            }
        }
        this._hash = new UrlHash_1.UrlHash(hashValues);
        return this;
    };
    UrlHashBuilder.prototype.addQueryFromObject = function (obj) {
        return this._addQueryFromObject(obj, '');
    };
    UrlHashBuilder.prototype._addQueryFromObject = function (obj, prefix) {
        if (obj) {
            for (var prop in obj) {
                if (obj.hasOwnProperty(prop)) {
                    var k = prefix ? prefix + "[" + prop + "]" : prop;
                    var propValue = obj[prop];
                    if (propValue !== null && typeof propValue === "object") {
                        this._addQueryFromObject(propValue, k);
                    }
                    else {
                        this.addQuery(k, propValue);
                    }
                }
            }
        }
    };
    UrlHashBuilder.prototype.addQueryString = function (query) {
        this._hash = new UrlHash_1.UrlHash(query);
        return this;
    };
    UrlHashBuilder.prototype.toString = function () {
        return this._hash.toString();
    };
    return UrlHashBuilder;
}());
exports.UrlHashBuilder = UrlHashBuilder;
//# sourceMappingURL=UrlHashBuilder.js.map