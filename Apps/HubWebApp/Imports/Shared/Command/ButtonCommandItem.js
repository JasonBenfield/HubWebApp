"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ButtonCommandItem = void 0;
var tslib_1 = require("tslib");
var ButtonViewModel_1 = require("../Html/ButtonViewModel");
var Button_1 = require("../Html/Button");
var ContextualClass_1 = require("../ContextualClass");
var FaIcon_1 = require("../FaIcon");
var MarginCss_1 = require("../MarginCss");
var TextSpan_1 = require("../Html/TextSpan");
var ButtonCommandItem = /** @class */ (function (_super) {
    tslib_1.__extends(ButtonCommandItem, _super);
    function ButtonCommandItem(vm) {
        if (vm === void 0) { vm = new ButtonViewModel_1.ButtonViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.executeRequested = _this.clicked;
        _this.icon = new FaIcon_1.FaIcon().addToContainer(_this)
            .configure(function (icon) {
            icon.setMargin(MarginCss_1.MarginCss.end(1));
        });
        _this.textSpan = new TextSpan_1.TextSpan().addToContainer(_this);
        _this.active = '';
        vm.type('button');
        _this.addCssName('btn');
        _this.setContext(ContextualClass_1.ContextualClass.default);
        return _this;
    }
    ButtonCommandItem.offscreenSubmit = function (vm) {
        var item = new ButtonCommandItem(vm);
        item.makeOffscreenSubmit();
        return item;
    };
    ButtonCommandItem.prototype.positionIconRight = function () {
        this.icon.pullRight();
        this.icon.setMargin(MarginCss_1.MarginCss.start(1));
    };
    ButtonCommandItem.prototype.setText = function (text) {
        this.textSpan.setText(text);
    };
    ButtonCommandItem.prototype.setActive = function () {
        this.updateActiveCss('active');
    };
    ButtonCommandItem.prototype.setInactive = function () {
        this.updateActiveCss('');
    };
    ButtonCommandItem.prototype.updateActiveCss = function (active) {
        this.replaceCssName(this.active, active);
        this.active = active;
    };
    ButtonCommandItem.prototype.changeTypeToSubmit = function () {
        this.vm.type('submit');
    };
    ButtonCommandItem.prototype.makeOffscreenSubmit = function () {
        this.addCssName('offscreen');
        this.changeTypeToSubmit();
    };
    return ButtonCommandItem;
}(Button_1.Button));
exports.ButtonCommandItem = ButtonCommandItem;
//# sourceMappingURL=ButtonCommandItem.js.map