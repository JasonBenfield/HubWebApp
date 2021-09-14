"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AggregateComponent = void 0;
var tslib_1 = require("tslib");
var Enumerable_1 = require("../Enumerable");
var AggregateComponent = /** @class */ (function () {
    function AggregateComponent(vm) {
        this.vm = vm;
        this.items = [];
    }
    AggregateComponent.prototype.setName = function (name) {
        this.vm.name(name);
    };
    AggregateComponent.prototype.configure = function (action) {
        action(this);
        return this;
    };
    AggregateComponent.prototype.addToContainer = function (container) {
        for (var _i = 0, _a = this.items; _i < _a.length; _i++) {
            var item = _a[_i];
            item.addToContainer(container);
        }
        return this;
    };
    AggregateComponent.prototype.insertIntoContainer = function (container, index) {
        for (var _i = 0, _a = this.items; _i < _a.length; _i++) {
            var item = _a[_i];
            item.insertIntoContainer(container, index);
            index++;
        }
        return this;
    };
    AggregateComponent.prototype.removeFromContainer = function (container) {
        for (var _i = 0, _a = this.items; _i < _a.length; _i++) {
            var item = _a[_i];
            item.removeFromContainer(container);
        }
        return this;
    };
    AggregateComponent.prototype.clear = function () {
        this.items.splice(0, this.items.length);
        this.vm.items([]);
    };
    AggregateComponent.prototype.prependItem = function (itemVM, create) {
        var item = create(itemVM);
        return this.insertItem(0, itemVM, item);
    };
    AggregateComponent.prototype.insertItemBefore = function (otherItem, item) {
        var index = this.indexOf(otherItem);
        return item.insertIntoContainer(this, index);
    };
    AggregateComponent.prototype.insertItemAfter = function (otherItem, item) {
        var index = this.indexOf(otherItem);
        return item.insertIntoContainer(this, index + 1);
    };
    AggregateComponent.prototype.moveItemBefore = function (sourceItem, targetItem) {
        this.removeItem(sourceItem);
        var targetIndex = this.indexOf(targetItem);
        return targetItem.insertIntoContainer(this, targetIndex);
    };
    AggregateComponent.prototype.moveItemAfter = function (sourceItem, targetItem) {
        this.removeItem(sourceItem);
        var targetIndex = this.indexOf(targetItem);
        return targetItem.insertIntoContainer(this, targetIndex + 1);
    };
    AggregateComponent.prototype.indexOf = function (item) {
        for (var i = 0; i <= this.items.length; i++) {
            if (this.items[i] === item) {
                return i;
            }
        }
        return -1;
    };
    AggregateComponent.prototype.addContent = function (item) {
        item.addToContainer(this);
        return item;
    };
    AggregateComponent.prototype.insertContent = function (index, item) {
        item.insertIntoContainer(this, index);
        return item;
    };
    AggregateComponent.prototype.addItem = function (itemVM, item) {
        return this.insertItem(this.items.length, itemVM, item);
    };
    AggregateComponent.prototype.insertItem = function (index, itemVM, item) {
        this.splice(index, 0, { component: item, vm: itemVM });
        return item;
    };
    AggregateComponent.prototype.removeItem = function (item) {
        var index = this.indexOf(item);
        this.splice(index, 1);
    };
    AggregateComponent.prototype.splice = function (index, deleteCount) {
        var _a, _b;
        var itemsToAdd = [];
        for (var _i = 2; _i < arguments.length; _i++) {
            itemsToAdd[_i - 2] = arguments[_i];
        }
        var itemVMs = new Enumerable_1.MappedArray(itemsToAdd, function (aggItem) { return aggItem.vm; }).value();
        (_a = this.vm.items).splice.apply(_a, tslib_1.__spreadArray([index, deleteCount], itemVMs));
        var items = new Enumerable_1.MappedArray(itemsToAdd, function (aggItem) { return aggItem.component; }).value();
        (_b = this.items).splice.apply(_b, tslib_1.__spreadArray([index, deleteCount], items));
    };
    AggregateComponent.prototype.show = function () { this.vm.isVisible(true); };
    AggregateComponent.prototype.hide = function () { this.vm.isVisible(false); };
    return AggregateComponent;
}());
exports.AggregateComponent = AggregateComponent;
//# sourceMappingURL=AggregateComponent.js.map