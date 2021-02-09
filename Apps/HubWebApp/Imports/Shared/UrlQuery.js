"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var NamedValue_1 = require("./NamedValue");
var _ = require("lodash");
var JoinedStrings_1 = require("./JoinedStrings");
var UrlQuery = /** @class */ (function () {
    function UrlQuery(query) {
        this.queryValues = [];
        if (query) {
            if (typeof query === 'string') {
                this.pushQueryValues(query);
            }
            else {
                this.queryValues = query;
            }
        }
    }
    UrlQuery.prototype.getValues = function () {
        return this.queryValues;
    };
    UrlQuery.prototype.getValue = function (name) {
        var queryValue = _(this.queryValues).find(function (qv) { return qv.name === name; });
        return queryValue ? queryValue.value : null;
    };
    UrlQuery.prototype.pushQueryValues = function (query) {
        var _this = this;
        var parts = query.split('&');
        _(parts).forEach(function (part) {
            var nameValue = part.split('=');
            var name = nameValue[0];
            var value = '';
            if (nameValue[1]) {
                value = nameValue[1];
            }
            _this.queryValues.push(new NamedValue_1.NamedValue(name, value));
        });
    };
    UrlQuery.prototype.hasQuery = function (name) {
        var queryValue = _(this.queryValues).find(function (qv) { return qv.name === name; });
        return Boolean(queryValue);
    };
    UrlQuery.prototype.toString = function () {
        var str = '';
        if (this.queryValues.length > 0) {
            str = new JoinedStrings_1.JoinedStrings('&', this.queryValues).value();
        }
        return str;
    };
    return UrlQuery;
}());
exports.UrlQuery = UrlQuery;
//# sourceMappingURL=UrlQuery.js.map