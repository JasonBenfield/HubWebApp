"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UrlBuilder = void 0;
var UrlQueryBuilder_1 = require("./UrlQueryBuilder");
var JoinedStrings_1 = require("./JoinedStrings");
var Url_1 = require("./Url");
var UrlHashBuilder_1 = require("./UrlHashBuilder");
var UrlBuilder = /** @class */ (function () {
    function UrlBuilder(baseUrl) {
        this._url = new Url_1.Url(baseUrl);
        this._query = new UrlQueryBuilder_1.UrlQueryBuilder(this._url.query);
        this._hash = new UrlHashBuilder_1.UrlHashBuilder(this._url.hash);
    }
    UrlBuilder.current = function () { return new UrlBuilder(location.href); };
    Object.defineProperty(UrlBuilder.prototype, "url", {
        get: function () { return this._url; },
        enumerable: false,
        configurable: true
    });
    UrlBuilder.prototype.addPart = function (part) {
        if (part) {
            var hashIndex = part.indexOf('#');
            if (hashIndex > -1) {
                var hashValue = part.substr(hashIndex + 1);
                this.addHashString(hashValue);
                part = part.substr(0, hashIndex);
            }
            var queryIndex = part.indexOf('?');
            if (queryIndex > -1) {
                var query = part.substr(queryIndex + 1);
                this.addQueryString(query);
                part = part.substr(0, queryIndex);
            }
            var baseUrl = this._url.withoutQueryAndHash();
            if (baseUrl.substr(baseUrl.length - 1) === '/') {
                baseUrl = baseUrl.substr(0, baseUrl.length - 1);
            }
            var parts = [baseUrl];
            var fragments = part.split('/');
            for (var _i = 0, fragments_1 = fragments; _i < fragments_1.length; _i++) {
                var fragment = fragments_1[_i];
                if (fragment) {
                    parts.push(fragment);
                }
            }
            this._url = new Url_1.Url(new JoinedStrings_1.JoinedStrings('/', parts).value());
        }
        return this;
    };
    UrlBuilder.prototype.hasQuery = function (name) {
        return this._query.hasQuery(name);
    };
    UrlBuilder.prototype.clearQuery = function () {
        this._query.clear();
        return this;
    };
    UrlBuilder.prototype.removeQuery = function (name) {
        this._query.removeQuery(name);
        return this;
    };
    UrlBuilder.prototype.replaceQuery = function (name, value) {
        this._query.replaceQuery(name, value);
        return this;
    };
    UrlBuilder.prototype.addQuery = function (name, value) {
        this._query.addQuery(name, value);
        return this;
    };
    UrlBuilder.prototype.addQueryString = function (query) {
        this._query.addQueryString(query);
        return this;
    };
    UrlBuilder.prototype.addQueryFromObject = function (obj) {
        this._query.addQueryFromObject(obj);
        return this;
    };
    UrlBuilder.prototype.getQuery = function () {
        return this._query;
    };
    UrlBuilder.prototype.getQueryValue = function (name) {
        return this._query.getValue(name);
    };
    UrlBuilder.prototype.hasHash = function (name) {
        return this._hash.hasQuery(name);
    };
    UrlBuilder.prototype.clearHash = function () {
        this._hash.clear();
    };
    UrlBuilder.prototype.removeHash = function (name) {
        this._hash.removeQuery(name);
        return this;
    };
    UrlBuilder.prototype.replaceHash = function (name, value) {
        this._hash.replaceQuery(name, value);
        return this;
    };
    UrlBuilder.prototype.addHash = function (name, value) {
        this._hash.addQuery(name, value);
        return this;
    };
    UrlBuilder.prototype.addHashString = function (query) {
        this._hash.addQueryString(query);
        return this;
    };
    UrlBuilder.prototype.addHashFromObject = function (obj) {
        this._hash.addQueryFromObject(obj);
        return this;
    };
    Object.defineProperty(UrlBuilder.prototype, "hash", {
        get: function () {
            return this._hash;
        },
        enumerable: false,
        configurable: true
    });
    UrlBuilder.prototype.getHashValue = function (name) {
        return this._hash.getValue(name);
    };
    UrlBuilder.prototype.value = function () {
        return this._url.value();
    };
    UrlBuilder.prototype.withoutQueryAndHash = function () {
        return this._url;
    };
    UrlBuilder.prototype.toString = function () {
        return this.value();
    };
    return UrlBuilder;
}());
exports.UrlBuilder = UrlBuilder;
//# sourceMappingURL=UrlBuilder.js.map