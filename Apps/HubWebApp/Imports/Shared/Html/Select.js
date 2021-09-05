"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Select = void 0;
var tslib_1 = require("tslib");
var HtmlComponent_1 = require("./HtmlComponent");
var ContextualClass_1 = require("../ContextualClass");
var SelectViewModel_1 = require("./SelectViewModel");
var Events_1 = require("../Events");
var Select = /** @class */ (function (_super) {
    tslib_1.__extends(Select, _super);
    function Select(vm) {
        if (vm === void 0) { vm = new SelectViewModel_1.SelectViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this._changed = new Events_1.DefaultEvent(_this);
        _this.changed = _this._changed.handler();
        _this.border = ContextualClass_1.ContextualClass.default;
        vm.value.subscribe(_this.onChange.bind(_this));
        return _this;
    }
    Select.prototype.onChange = function (value) {
        this._changed.invoke(value);
    };
    Select.prototype.enable = function () { this.vm.isEnabled(true); };
    Select.prototype.disable = function () { this.vm.isEnabled(false); };
    Select.prototype.setBorder = function (border) {
        var borderCss = this.getBorderCss(border);
        this.replaceCssName(this.getBorderCss(this.border), borderCss);
        this.border = border;
    };
    Select.prototype.getBorderCss = function (border) {
        return border === ContextualClass_1.ContextualClass.default ? '' : border.append('border');
    };
    Select.prototype.setItemCaption = function (itemCaption) {
        this.vm.itemsCaption(itemCaption);
    };
    Select.prototype.setItems = function (items) {
        this.vm.items(items);
    };
    Select.prototype.getValue = function () { return this.vm.value(); };
    Select.prototype.setValue = function (value) {
        this.vm.value(value);
    };
    return Select;
}(HtmlComponent_1.HtmlComponent));
exports.Select = Select;
//# sourceMappingURL=Select.js.map