"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var NamedValue_1 = require("./NamedValue");
var _ = require("lodash");
var UrlQuery_1 = require("./UrlQuery");
var UrlQueryBuilder = /** @class */ (function () {
    function UrlQueryBuilder(query) {
        if (typeof query === 'string') {
            this._query = new UrlQuery_1.UrlQuery(query);
        }
        else {
            this._query = query;
        }
    }
    Object.defineProperty(UrlQueryBuilder.prototype, "query", {
        get: function () { return this._query; },
        enumerable: true,
        configurable: true
    });
    UrlQueryBuilder.prototype.getValues = function () {
        return this._query.getValues();
    };
    UrlQueryBuilder.prototype.getValue = function (name) {
        return this._query.getValue(name);
    };
    UrlQueryBuilder.prototype.clear = function () {
        this._query = new UrlQuery_1.UrlQuery('');
        return this;
    };
    UrlQueryBuilder.prototype.hasQuery = function (name) {
        return this._query.hasQuery(name);
    };
    UrlQueryBuilder.prototype.removeQuery = function (name) {
        var queryValues = this._query.getValues();
        for (var i = queryValues.length - 1; i >= 0; i--) {
            var queryPart = queryValues[i];
            if (queryPart.name === name) {
                queryValues.splice(i, 1);
            }
        }
        this._query = new UrlQuery_1.UrlQuery(queryValues);
        return this;
    };
    UrlQueryBuilder.prototype.replaceQuery = function (name, value) {
        this.removeQuery(name);
        return this.addQuery(name, value);
    };
    UrlQueryBuilder.prototype.addQuery = function (name, value) {
        var _this = this;
        var queryValues = this._query.getValues();
        if (name) {
            if (value instanceof Date) {
                var queryValue = value === undefined || value === null
                    ? null
                    : value.toISOString();
                queryValues.push(new NamedValue_1.NamedValue(name, queryValue));
            }
            else if (typeof value === 'string') {
                var queryValue = void 0;
                if (value !== undefined && value !== null) {
                    queryValue = value;
                }
                queryValues.push(new NamedValue_1.NamedValue(name, queryValue));
            }
            else if (typeof value === 'number') {
                var queryValue = void 0;
                if (value !== undefined && value !== null) {
                    queryValue = value.toString();
                }
                queryValues.push(new NamedValue_1.NamedValue(name, queryValue));
            }
            else if (_.isArray(value)) {
                _(value).forEach(function (arrValue) {
                    _this.addQuery(name, arrValue);
                });
            }
            else {
                queryValues.push(new NamedValue_1.NamedValue(name, value && value.toString()));
            }
        }
        this._query = new UrlQuery_1.UrlQuery(queryValues);
        return this;
    };
    UrlQueryBuilder.prototype.addQueryFromObject = function (obj) {
        return this._addQueryFromObject(obj, '');
    };
    UrlQueryBuilder.prototype._addQueryFromObject = function (obj, prefix) {
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
    UrlQueryBuilder.prototype.addQueryString = function (query) {
        this._query = new UrlQuery_1.UrlQuery(query);
        return this;
    };
    UrlQueryBuilder.prototype.toString = function () {
        return this._query.toString();
    };
    return UrlQueryBuilder;
}());
exports.UrlQueryBuilder = UrlQueryBuilder;
//# sourceMappingURL=UrlQueryBuilder.js.map