"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FaIcon = exports.FaIconNames = exports.FaIconViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("./ComponentTemplate");
var template = require("./Templates/FaIcon.html");
var ContextualClass_1 = require("./ContextualClass");
var HtmlComponent_1 = require("./Html/HtmlComponent");
var HtmlComponentViewModel_1 = require("./Html/HtmlComponentViewModel");
var FaIconViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(FaIconViewModel, _super);
    function FaIconViewModel() {
        return _super.call(this, new ComponentTemplate_1.ComponentTemplate('fa-icon', template)) || this;
    }
    return FaIconViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.FaIconViewModel = FaIconViewModel;
var FaIconNames = /** @class */ (function () {
    function FaIconNames() {
    }
    FaIconNames.login = 'sign-in-alt';
    FaIconNames.save = 'check';
    FaIconNames.cancel = 'times';
    return FaIconNames;
}());
exports.FaIconNames = FaIconNames;
var FaIcon = /** @class */ (function (_super) {
    tslib_1.__extends(FaIcon, _super);
    function FaIcon(name, vm) {
        if (name === void 0) { name = ''; }
        if (vm === void 0) { vm = new FaIconViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.color = '';
        _this.size = '';
        _this.rotation = '';
        _this.pulled = '';
        _this.animation = '';
        _this.solidStyle();
        _this.setName(name);
        return _this;
    }
    FaIcon.prototype.setColor = function (color) {
        if (color === void 0) { color = ContextualClass_1.ContextualClass.default; }
        var colorCss = color === ContextualClass_1.ContextualClass.default ? '' : color.append('text');
        this.replaceCssName(this.color, colorCss);
        this.color = colorCss;
    };
    FaIcon.prototype.regularStyle = function () {
        this.setPrefix('far');
    };
    FaIcon.prototype.solidStyle = function () {
        this.setPrefix('fas');
    };
    FaIcon.prototype.setPrefix = function (prefix) {
        this.replaceCssName(this.prefix, prefix);
        this.prefix = prefix;
    };
    FaIcon.prototype.setName = function (name) {
        name = this.normalizeName(name);
        this.replaceCssName(this.name, name);
        this.name = name;
    };
    FaIcon.prototype.normalizeName = function (name) {
        if (name && name.indexOf('fa-') !== 0) {
            name = "fa-" + name;
        }
        return name;
    };
    FaIcon.prototype.makeFixedWidth = function () {
        this.addCssName('fa-fw');
    };
    FaIcon.prototype.resize = function (size) {
        var sizeCss = size ? "fa-" + size : size;
        this.replaceCssName(this.size, sizeCss);
        this.size = sizeCss;
    };
    FaIcon.prototype.makeBordered = function () {
        this.addCssName('fa-bordered');
    };
    FaIcon.prototype.rotate = function (howMuch) {
        var rotationCss = howMuch ? "fa-rotate-" + howMuch : '';
        this.replaceCssName(this.rotation, rotationCss);
    };
    FaIcon.prototype.pullLeft = function () {
        this.pull('fa-pull-left');
    };
    FaIcon.prototype.pullRight = function () {
        this.pull('fa-pull-right');
    };
    FaIcon.prototype.pull = function (pulled) {
        this.replaceCssName(this.pulled, pulled);
        this.pulled = pulled;
    };
    FaIcon.prototype.startAnimation = function (animation) {
        var css = animation ? "fa-" + animation : animation;
        this.replaceCssName(this.animation, css);
        this.animation = css;
    };
    FaIcon.prototype.stopAnimation = function () {
        this.removeCssName(this.animation);
        this.animation = '';
    };
    return FaIcon;
}(HtmlComponent_1.HtmlComponent));
exports.FaIcon = FaIcon;
//# sourceMappingURL=FaIcon.js.map