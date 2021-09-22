"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Input = void 0;
var tslib_1 = require("tslib");
var HtmlComponent_1 = require("./HtmlComponent");
var ContextualClass_1 = require("../ContextualClass");
var InputViewModel_1 = require("./InputViewModel");
var Events_1 = require("../Events");
var Input = /** @class */ (function (_super) {
    tslib_1.__extends(Input, _super);
    function Input(vm) {
        if (vm === void 0) { vm = new InputViewModel_1.InputViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this._changed = new Events_1.DefaultEvent(_this);
        _this.changed = _this._changed.handler();
        _this.border = ContextualClass_1.ContextualClass.default;
        vm.type('text');
        vm.value.subscribe(_this.onValueChanged.bind(_this));
        return _this;
    }
    Input.prototype.onValueChanged = function (value) {
        this._changed.invoke(value);
    };
    Input.prototype.enable = function () { this.vm.isEnabled(true); };
    Input.prototype.disable = function () { this.vm.isEnabled(false); };
    Input.prototype.clearAutocomplete = function () { this.setAutocomplete(null); };
    Input.prototype.setAutocompleteOff = function () { this.setAutocomplete('off'); };
    Input.prototype.setAutocompleteNewPassword = function () { this.setAutocomplete('new-password'); };
    Input.prototype.setAutocomplete = function (autocomplete) {
        this.vm.autocomplete(autocomplete);
    };
    Input.prototype.getValue = function () {
        return this.value;
    };
    Input.prototype.setValue = function (value) {
        this.value = value;
        this.vm.value(value);
    };
    Input.prototype.setBorder = function (border) {
        var borderCss = this.getBorderCss(border);
        this.replaceCssName(this.getBorderCss(this.border), borderCss);
        this.border = border;
    };
    Input.prototype.getBorderCss = function (border) {
        return border === ContextualClass_1.ContextualClass.default ? '' : border.append('border');
    };
    Input.prototype.setMaxLength = function (maxLength) {
        this.vm.maxLength(maxLength);
    };
    Input.prototype.setType = function (type) {
        this.vm.type(type);
    };
    Input.prototype.setFocus = function () { this.vm.hasFocus(true); };
    Input.prototype.blur = function () { this.vm.hasFocus(false); };
    return Input;
}(HtmlComponent_1.HtmlComponent));
exports.Input = Input;
//# sourceMappingURL=Input.js.map