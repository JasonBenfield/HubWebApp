"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ColumnCss = exports.ColumnCssForBreakpoint = void 0;
var CssClass_1 = require("./CssClass");
var ColumnCssForBreakpoint = /** @class */ (function () {
    function ColumnCssForBreakpoint(breakpoint, size) {
        this.breakpoint = breakpoint;
        this.size = size;
    }
    ColumnCssForBreakpoint.prototype.cssClassName = function () {
        var css = 'col';
        if (this.breakpoint && this.breakpoint !== 'xs') {
            css += "-" + this.breakpoint;
        }
        if (this.size === 'auto') {
            css += '-auto';
        }
        else if (this.size && this.size !== 'fill') {
            css += "-" + this.size;
        }
        return css;
    };
    ColumnCssForBreakpoint.prototype.toString = function () {
        return this.cssClassName();
    };
    return ColumnCssForBreakpoint;
}());
exports.ColumnCssForBreakpoint = ColumnCssForBreakpoint;
var ColumnCss = /** @class */ (function () {
    function ColumnCss() {
        this.breakpoints = {};
    }
    ColumnCss.xs = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        return new ColumnCss().xs(columnSize);
    };
    ColumnCss.sm = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        return new ColumnCss().sm(columnSize);
    };
    ColumnCss.lg = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        return new ColumnCss().lg(columnSize);
    };
    ColumnCss.xl = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        return new ColumnCss().xl(columnSize);
    };
    ColumnCss.xxl = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        return new ColumnCss().xxl(columnSize);
    };
    ColumnCss.prototype.xs = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        this.breakpoints.xs = new ColumnCssForBreakpoint('xs', columnSize);
        return this;
    };
    ColumnCss.prototype.sm = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        this.breakpoints.sm = new ColumnCssForBreakpoint('sm', columnSize);
    };
    ColumnCss.prototype.md = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        this.breakpoints.md = new ColumnCssForBreakpoint('md', columnSize);
    };
    ColumnCss.prototype.lg = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        this.breakpoints.lg = new ColumnCssForBreakpoint('lg', columnSize);
    };
    ColumnCss.prototype.xl = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        this.breakpoints.xl = new ColumnCssForBreakpoint('xl', columnSize);
    };
    ColumnCss.prototype.xxl = function (columnSize) {
        if (columnSize === void 0) { columnSize = 'fill'; }
        this.breakpoints.xxl = new ColumnCssForBreakpoint('xxl', columnSize);
    };
    ColumnCss.prototype.cssClass = function () {
        var css = new CssClass_1.CssClass();
        css.addName(this.breakpoints.xs && this.breakpoints.xs.cssClassName());
        css.addName(this.breakpoints.sm && this.breakpoints.sm.cssClassName());
        css.addName(this.breakpoints.md && this.breakpoints.md.cssClassName());
        css.addName(this.breakpoints.lg && this.breakpoints.lg.cssClassName());
        css.addName(this.breakpoints.xl && this.breakpoints.xl.cssClassName());
        css.addName(this.breakpoints.xxl && this.breakpoints.xxl.cssClassName());
        return css;
    };
    ColumnCss.prototype.toString = function () {
        return this.cssClass().toString();
    };
    return ColumnCss;
}());
exports.ColumnCss = ColumnCss;
//# sourceMappingURL=ColumnCss.js.map