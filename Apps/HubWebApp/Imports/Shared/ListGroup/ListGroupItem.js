"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ContextualClass_1 = require("../ContextualClass");
var AggregateComponent_1 = require("../Html/AggregateComponent");
var HtmlComponent_1 = require("../Html/HtmlComponent");
var ListGroupItem = /** @class */ (function (_super) {
    tslib_1.__extends(ListGroupItem, _super);
    function ListGroupItem(vm) {
        var _this = _super.call(this, vm) || this;
        _this.content = new AggregateComponent_1.AggregateComponent(_this.vm.content);
        _this.contextClass = ContextualClass_1.ContextualClass.default;
        _this.active = '';
        _this.addCssName('list-group-item');
        if (vm.isClickable) {
            _this.addCssName('list-group-item-action');
        }
        return _this;
    }
    ListGroupItem.prototype.getData = function () { return this.data; };
    ListGroupItem.prototype.setData = function (data) { this.data = data; };
    ListGroupItem.prototype.addToList = function (list) {
        list.addListItem(this.vm, this);
        return this;
    };
    ListGroupItem.prototype.addContent = function (item) {
        return item.addToContainer(this.content);
    };
    ListGroupItem.prototype.setContext = function (contextClass) {
        var newCss = this.getCss(contextClass);
        this.replaceCssName(this.getCss(this.contextClass), newCss);
        this.contextClass = contextClass;
    };
    ListGroupItem.prototype.getCss = function (contextClass) {
        return contextClass && !contextClass.equals(ContextualClass_1.ContextualClass.default) ?
            contextClass.toString() : '';
    };
    ListGroupItem.prototype.activate = function () {
        this.setActive('active');
    };
    ListGroupItem.prototype.deactivate = function () {
        this.setActive('');
    };
    ListGroupItem.prototype.setActive = function (active) {
        if (this.active) {
            this.removeCssName(this.active);
        }
        this.active = active;
        this.addCssName(this.active);
    };
    return ListGroupItem;
}(HtmlComponent_1.HtmlComponent));
exports.ListGroupItem = ListGroupItem;
//# sourceMappingURL=ListGroupItem.js.map