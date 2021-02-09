"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var HtmlComponent_1 = require("./HtmlComponent");
var HtmlContainerComponent = /** @class */ (function (_super) {
    tslib_1.__extends(HtmlContainerComponent, _super);
    function HtmlContainerComponent(vm, content) {
        if (content === void 0) { content = new AggregateComponent_1.AggregateComponent(vm.content); }
        var _this = _super.call(this, vm) || this;
        _this.vm = vm;
        _this.content = content;
        return _this;
    }
    HtmlContainerComponent.prototype.addContent = function (item) {
        item.addToContainer(this.content);
        return item;
    };
    HtmlContainerComponent.prototype.insertContent = function (index, item) {
        item.insertIntoContainer(this.content, index);
        return item;
    };
    HtmlContainerComponent.prototype.addItem = function (itemVM, item) {
        return this.content.addItem(itemVM, item);
    };
    HtmlContainerComponent.prototype.insertItem = function (index, itemVM, item) {
        return this.content.insertItem(index, itemVM, item);
    };
    HtmlContainerComponent.prototype.removeItem = function (item) {
        return this.content.removeItem(item);
    };
    HtmlContainerComponent.prototype.addToContainer = function (container) {
        return container.addItem(this.vm, this);
    };
    HtmlContainerComponent.prototype.insertIntoContainer = function (container, index) {
        return container.insertItem(index, this.vm, this);
    };
    HtmlContainerComponent.prototype.removeFromContainer = function (container) {
        return container.removeItem(this);
    };
    return HtmlContainerComponent;
}(HtmlComponent_1.HtmlComponent));
exports.HtmlContainerComponent = HtmlContainerComponent;
//# sourceMappingURL=HtmlContainerComponent.js.map