"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EnumerableRange = exports.Any = exports.First = exports.FilteredArray = exports.MappedArray = exports.EnumerableArray = void 0;
var _ = require("lodash");
var EnumerableArray = /** @class */ (function () {
    function EnumerableArray(source) {
        this.source = source;
    }
    EnumerableArray.create = function (source) {
        if (_.isArray(source)) {
            return new EnumerableArray(source);
        }
        return source;
    };
    EnumerableArray.prototype.value = function () {
        if (_.isArray(this.source)) {
            return this.source;
        }
        return this.source.value();
    };
    EnumerableArray.prototype.isArray = function () { return _.isArray(this.source); };
    return EnumerableArray;
}());
exports.EnumerableArray = EnumerableArray;
var MappedArray = /** @class */ (function () {
    function MappedArray(source, map) {
        this.map = map;
        this.source = EnumerableArray.create(source);
    }
    MappedArray.prototype.value = function () { return _.map(this.source.value(), this.map); };
    return MappedArray;
}());
exports.MappedArray = MappedArray;
var FilteredArray = /** @class */ (function () {
    function FilteredArray(source, isMatch) {
        this.isMatch = isMatch;
        this.source = EnumerableArray.create(source);
    }
    FilteredArray.prototype.value = function () { return _.filter(this.source.value(), this.isMatch); };
    return FilteredArray;
}());
exports.FilteredArray = FilteredArray;
var First = /** @class */ (function () {
    function First(source) {
        this.source = EnumerableArray.create(source);
    }
    First.prototype.value = function () { return this.source.value()[0]; };
    return First;
}());
exports.First = First;
var Any = /** @class */ (function () {
    function Any(source) {
        this.source = EnumerableArray.create(source);
    }
    Any.prototype.value = function () { return this.source.value().length > 0; };
    return Any;
}());
exports.Any = Any;
var EnumerableRange = /** @class */ (function () {
    function EnumerableRange(start, count) {
        this.source = [];
        for (var i = start; i < start + count; i++) {
            this.source.push(i);
        }
    }
    EnumerableRange.prototype.value = function () { return this.source; };
    return EnumerableRange;
}());
exports.EnumerableRange = EnumerableRange;
//# sourceMappingURL=Enumerable.js.map