"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var UrlHash_1 = require("./UrlHash");
var UrlQuery_1 = require("./UrlQuery");
var Url = /** @class */ (function () {
    function Url(baseUrl) {
        this.url = baseUrl;
        var hashIndex = this.url.indexOf('#');
        this._hash = new UrlHash_1.UrlHash(hashIndex > -1 ? this.url.substr(hashIndex + 1) : '');
        if (hashIndex > -1) {
            this.url = this.url.substr(0, hashIndex);
        }
        var queryIndex = this.url.indexOf('?');
        this._query = new UrlQuery_1.UrlQuery(queryIndex > -1 ? this.url.substr(queryIndex + 1) : '');
        if (queryIndex > -1) {
            this.url = this.url.substr(0, queryIndex);
        }
    }
    Url.current = function () { return new Url(location.href); };
    Url.prototype.hasQuery = function (name) {
        return this._query.hasQuery(name);
    };
    Object.defineProperty(Url.prototype, "query", {
        get: function () {
            return this._query;
        },
        enumerable: true,
        configurable: true
    });
    Url.prototype.getQueryValue = function (name) {
        return this._query.getValue(name);
    };
    Object.defineProperty(Url.prototype, "hash", {
        get: function () {
            return this._hash;
        },
        enumerable: true,
        configurable: true
    });
    Url.prototype.hasHash = function (name) {
        return this._hash.hasQuery(name);
    };
    Url.prototype.getHashValue = function (name) {
        return this._hash.getValue(name);
    };
    Url.prototype.value = function () {
        var url = this.url;
        var queryString = this._query.toString();
        if (queryString) {
            url += "?" + queryString;
        }
        return url;
    };
    Url.prototype.withoutQueryAndHash = function () {
        return this.url;
    };
    Url.prototype.toString = function () {
        return this.value();
    };
    return Url;
}());
exports.Url = Url;
//# sourceMappingURL=Url.js.map