"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BaseList = void 0;
var tslib_1 = require("tslib");
var Enumerable_1 = require("../Enumerable");
var Events_1 = require("../Events");
var HtmlComponent_1 = require("./HtmlComponent");
var BaseList = /** @class */ (function (_super) {
    tslib_1.__extends(BaseList, _super);
    function BaseList(createItem, createItemVM, vm) {
        var _this = _super.call(this, vm) || this;
        _this.createItem = createItem;
        _this.createItemVM = createItemVM;
        _this.items = [];
        _this._itemClicked = new Events_1.DefaultEvent(_this);
        _this.itemClicked = _this._itemClicked.handler();
        _this.vm.itemClicked.register(_this.onItemClicked.bind(_this));
        return _this;
    }
    BaseList.prototype.onItemClicked = function (itemVM) {
        var index = this.vm.items.indexOf(itemVM);
        if (index >= 0) {
            this._itemClicked.invoke(this.items[index]);
        }
    };
    BaseList.prototype.clear = function () {
        this.items.splice(0, this.items.length);
        this.vm.items([]);
        this.vm.hasItems(false);
    };
    BaseList.prototype.addItem = function () {
        var itemVM = this.createItemVM();
        return this.add(itemVM, this.createItem);
    };
    BaseList.prototype.add = function (itemVM, create) {
        return this.addListItem(itemVM, create(itemVM));
    };
    BaseList.prototype.addListItem = function (itemVM, item) {
        this.vm.items.push(itemVM);
        this.vm.hasItems(true);
        this.items.push(item);
        return item;
    };
    BaseList.prototype.setItems = function (sourceItems, config) {
        var _a;
        var _this = this;
        var itemVMs = [];
        var items = new Enumerable_1.MappedArray(sourceItems, function (sourceItem) {
            var itemVM = _this.createItemVM();
            itemVMs.push(itemVM);
            var item = _this.createItem(itemVM);
            config(sourceItem, item);
            return item;
        }).value();
        (_a = this.items).splice.apply(_a, tslib_1.__spreadArray([0, this.items.length], items));
        this.vm.items(itemVMs);
        this.vm.hasItems(itemVMs.length > 0);
    };
    return BaseList;
}(HtmlComponent_1.HtmlComponent));
exports.BaseList = BaseList;
//# sourceMappingURL=BaseList.js.map