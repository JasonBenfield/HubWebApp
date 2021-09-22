"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PaddingCss = void 0;
var CssClass_1 = require("./CssClass");
var PaddingCss = /** @class */ (function () {
    function PaddingCss() {
        this.css = new CssClass_1.CssClass;
    }
    PaddingCss.bottom = function (amount) {
        return PaddingCss.xs({ bottom: amount });
    };
    PaddingCss.top = function (amount) {
        return PaddingCss.xs({ top: amount });
    };
    PaddingCss.start = function (amount) {
        return PaddingCss.xs({ start: amount });
    };
    PaddingCss.end = function (amount) {
        return PaddingCss.xs({ end: amount });
    };
    PaddingCss.xs = function (amounts) {
        return new PaddingCss().xs(amounts);
    };
    PaddingCss.sm = function (amounts) {
        return new PaddingCss().sm(amounts);
    };
    PaddingCss.md = function (amounts) {
        return new PaddingCss().md(amounts);
    };
    PaddingCss.lg = function (amounts) {
        return new PaddingCss().lg(amounts);
    };
    PaddingCss.xl = function (amounts) {
        return new PaddingCss().xl(amounts);
    };
    PaddingCss.xxl = function (amounts) {
        return new PaddingCss().xxl(amounts);
    };
    PaddingCss.prototype.xs = function (amounts) {
        this.addCssForBreakpoint('xs', amounts);
        return this;
    };
    PaddingCss.prototype.sm = function (amounts) {
        this.addCssForBreakpoint('sm', amounts);
        return this;
    };
    PaddingCss.prototype.md = function (amounts) {
        this.addCssForBreakpoint('md', amounts);
        return this;
    };
    PaddingCss.prototype.lg = function (amounts) {
        this.addCssForBreakpoint('lg', amounts);
        return this;
    };
    PaddingCss.prototype.xl = function (amounts) {
        this.addCssForBreakpoint('xl', amounts);
        return this;
    };
    PaddingCss.prototype.xxl = function (amounts) {
        this.addCssForBreakpoint('xxl', amounts);
        return this;
    };
    PaddingCss.prototype.addCssForBreakpoint = function (breakpoint, amounts) {
        if (amounts !== null && amounts !== undefined) {
            if (this.isPaddingAmount(amounts)) {
                this.css.addName(this.getCss(breakpoint, '', amounts));
            }
            else {
                this.css.addName(this.getCss(breakpoint, 'b', amounts.bottom));
                this.css.addName(this.getCss(breakpoint, 't', amounts.top));
                this.css.addName(this.getCss(breakpoint, 's', amounts.start));
                this.css.addName(this.getCss(breakpoint, 'e', amounts.end));
            }
        }
    };
    PaddingCss.prototype.isPaddingAmount = function (data) {
        return typeof data === 'number' || data === 'auto';
    };
    PaddingCss.prototype.getCss = function (breakpoint, direction, amount) {
        var css;
        if (amount === undefined || amount == null) {
            css = '';
        }
        else {
            css = 'p';
            if (direction) {
                css += direction;
            }
            if (breakpoint && breakpoint !== 'xs') {
                css += "-" + breakpoint;
            }
            css += "-" + amount;
        }
        return css;
    };
    PaddingCss.prototype.cssClass = function () {
        return this.css;
    };
    PaddingCss.prototype.toString = function () {
        return this.css.toString();
    };
    return PaddingCss;
}());
exports.PaddingCss = PaddingCss;
//# sourceMappingURL=PaddingCss.js.map