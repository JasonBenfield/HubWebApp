"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CssClass_1 = require("./CssClass");
var AlignCssType = /** @class */ (function () {
    function AlignCssType(type) {
        this.type = type;
        this.breakpoints = {};
    }
    AlignCssType.prototype.xs = function (alignType) {
        this.breakpoints.xs = alignType;
    };
    AlignCssType.prototype.sm = function (alignType) {
        this.breakpoints.sm = alignType;
    };
    AlignCssType.prototype.md = function (alignType) {
        this.breakpoints.md = alignType;
    };
    AlignCssType.prototype.lg = function (alignType) {
        this.breakpoints.lg = alignType;
    };
    AlignCssType.prototype.xl = function (alignType) {
        this.breakpoints.xl = alignType;
    };
    AlignCssType.prototype.xxl = function (alignType) {
        this.breakpoints.xxl = alignType;
    };
    AlignCssType.prototype.cssClass = function () {
        var css = new CssClass_1.CssClass();
        css.addName(this.breakpoints.xs && this.getCssName('xs', this.breakpoints.xs));
        css.addName(this.breakpoints.sm && this.getCssName('sm', this.breakpoints.sm));
        css.addName(this.breakpoints.md && this.getCssName('md', this.breakpoints.md));
        css.addName(this.breakpoints.lg && this.getCssName('lg', this.breakpoints.lg));
        css.addName(this.breakpoints.xl && this.getCssName('xl', this.breakpoints.xl));
        css.addName(this.breakpoints.xxl && this.getCssName('xxl', this.breakpoints.xxl));
        return css;
    };
    AlignCssType.prototype.getCssName = function (size, alignType) {
        var cssName = '';
        if (size || alignType) {
            cssName = 'align';
        }
        if (this.type) {
            cssName += "-" + this.type;
        }
        if (size && size !== 'xs') {
            cssName += "-" + size;
        }
        if (alignType) {
            cssName += "-" + alignType;
        }
        return cssName;
    };
    AlignCssType.prototype.toString = function () {
        return this.cssClass().toString();
    };
    return AlignCssType;
}());
exports.AlignCssType = AlignCssType;
var AlignContentCssType = /** @class */ (function () {
    function AlignContentCssType() {
        this.breakpoints = {};
    }
    AlignContentCssType.prototype.xs = function (alignType) {
        this.breakpoints.xs = alignType;
    };
    AlignContentCssType.prototype.sm = function (alignType) {
        this.breakpoints.sm = alignType;
    };
    AlignContentCssType.prototype.md = function (alignType) {
        this.breakpoints.md = alignType;
    };
    AlignContentCssType.prototype.lg = function (alignType) {
        this.breakpoints.lg = alignType;
    };
    AlignContentCssType.prototype.xl = function (alignType) {
        this.breakpoints.xl = alignType;
    };
    AlignContentCssType.prototype.xxl = function (alignType) {
        this.breakpoints.xxl = alignType;
    };
    AlignContentCssType.prototype.cssClass = function () {
        var css = new CssClass_1.CssClass();
        css.addName(this.breakpoints.xs && this.getCssName('xs', this.breakpoints.xs));
        css.addName(this.breakpoints.sm && this.getCssName('sm', this.breakpoints.sm));
        css.addName(this.breakpoints.md && this.getCssName('md', this.breakpoints.md));
        css.addName(this.breakpoints.lg && this.getCssName('lg', this.breakpoints.lg));
        css.addName(this.breakpoints.xl && this.getCssName('xl', this.breakpoints.xl));
        css.addName(this.breakpoints.xxl && this.getCssName('xxl', this.breakpoints.xxl));
        return css;
    };
    AlignContentCssType.prototype.getCssName = function (size, alignType) {
        var cssName = '';
        if (size || alignType) {
            cssName = 'align';
        }
        cssName += "-content";
        if (size && size !== 'xs') {
            cssName += "-" + size;
        }
        if (alignType) {
            cssName += "-" + alignType;
        }
        return cssName;
    };
    AlignContentCssType.prototype.toString = function () {
        return this.cssClass().toString();
    };
    return AlignContentCssType;
}());
exports.AlignContentCssType = AlignContentCssType;
var AlignCss = /** @class */ (function () {
    function AlignCss() {
        this.types = {};
    }
    AlignCss.prototype.items = function (config) {
        this.types.items = new AlignCssType('items');
        config(this.types.items);
        return this;
    };
    AlignCss.prototype.content = function (config) {
        this.types.content = new AlignContentCssType();
        config(this.types.content);
        return this;
    };
    AlignCss.prototype.self = function (config) {
        this.types.self = new AlignCssType('self');
        config(this.types.self);
        return this;
    };
    AlignCss.prototype.cssClass = function () {
        var css = new CssClass_1.CssClass();
        css.addFrom(this.types.items && this.types.items.cssClass());
        css.addFrom(this.types.content && this.types.content.cssClass());
        css.addFrom(this.types.self && this.types.self.cssClass());
        return css;
    };
    AlignCss.prototype.toString = function () {
        return this.cssClass().toString();
    };
    return AlignCss;
}());
exports.AlignCss = AlignCss;
//# sourceMappingURL=AlignCss.js.map