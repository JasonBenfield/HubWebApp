"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MarginCss = void 0;
var CssClass_1 = require("./CssClass");
var MarginCss = /** @class */ (function () {
    function MarginCss() {
        this.css = new CssClass_1.CssClass;
    }
    MarginCss.bottom = function (amount) {
        return MarginCss.xs({ bottom: amount });
    };
    MarginCss.top = function (amount) {
        return MarginCss.xs({ top: amount });
    };
    MarginCss.start = function (amount) {
        return MarginCss.xs({ start: amount });
    };
    MarginCss.end = function (amount) {
        return MarginCss.xs({ end: amount });
    };
    MarginCss.xs = function (amounts) {
        return new MarginCss().xs(amounts);
    };
    MarginCss.sm = function (amounts) {
        return new MarginCss().sm(amounts);
    };
    MarginCss.md = function (amounts) {
        return new MarginCss().md(amounts);
    };
    MarginCss.lg = function (amounts) {
        return new MarginCss().lg(amounts);
    };
    MarginCss.xl = function (amounts) {
        return new MarginCss().xl(amounts);
    };
    MarginCss.xxl = function (amounts) {
        return new MarginCss().xxl(amounts);
    };
    MarginCss.prototype.xs = function (amounts) {
        this.addCssForBreakpoint('xs', amounts);
        return this;
    };
    MarginCss.prototype.sm = function (amounts) {
        this.addCssForBreakpoint('sm', amounts);
        return this;
    };
    MarginCss.prototype.md = function (amounts) {
        this.addCssForBreakpoint('md', amounts);
        return this;
    };
    MarginCss.prototype.lg = function (amounts) {
        this.addCssForBreakpoint('lg', amounts);
        return this;
    };
    MarginCss.prototype.xl = function (amounts) {
        this.addCssForBreakpoint('xl', amounts);
        return this;
    };
    MarginCss.prototype.xxl = function (amounts) {
        this.addCssForBreakpoint('xxl', amounts);
        return this;
    };
    MarginCss.prototype.addCssForBreakpoint = function (breakpoint, amounts) {
        if (amounts !== null && amounts !== undefined) {
            if (this.isMarginAmount(amounts)) {
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
    MarginCss.prototype.isMarginAmount = function (data) {
        return typeof data === 'number' || data === 'auto';
    };
    MarginCss.prototype.getCss = function (breakpoint, direction, amount) {
        var css;
        if (amount === undefined) {
            css = '';
        }
        else {
            css = 'm';
            if (direction) {
                css += direction;
            }
            if (amount === null || amount === undefined) {
                amount = 0;
            }
            if (breakpoint && breakpoint !== 'xs') {
                css += "-" + breakpoint;
            }
            css += "-" + amount;
        }
        return css;
    };
    MarginCss.prototype.cssClass = function () {
        return this.css;
    };
    MarginCss.prototype.toString = function () {
        return this.css.toString();
    };
    return MarginCss;
}());
exports.MarginCss = MarginCss;
//# sourceMappingURL=MarginCss.js.map