"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ContextualClass_1 = require("./ContextualClass");
var CssClass_1 = require("./CssClass");
var TextCss = /** @class */ (function () {
    function TextCss() {
        this.aligns = {};
    }
    TextCss.prototype.start = function () {
        this.aligns.xs = 'text-start';
        return this;
    };
    TextCss.prototype.end = function () {
        this.aligns.xs = 'text-end';
        return this;
    };
    TextCss.prototype.center = function () {
        this.aligns.xs = 'text-center';
        return this;
    };
    TextCss.prototype.context = function (context) {
        this._color = context;
        return this;
    };
    TextCss.prototype.muted = function () {
        this._color = 'text-muted';
        return this;
    };
    TextCss.prototype.resetColor = function () {
        this._color = 'text-reset';
        return this;
    };
    TextCss.prototype.truncate = function () {
        this._truncate = 'text-truncate';
        return this;
    };
    TextCss.prototype.bold = function () {
        this.fontWeight = 'fw-bold';
        return this;
    };
    TextCss.prototype.bolder = function () {
        this.fontWeight = 'fw-bolder';
        return this;
    };
    TextCss.prototype.fontSize = function (size) {
        this.size = size ? "fs-" + size : null;
    };
    TextCss.prototype.italicize = function () {
        this.style = 'fst-italic';
    };
    TextCss.prototype.cssClass = function () {
        var css = new CssClass_1.CssClass();
        if (this._color) {
            if (this._color instanceof ContextualClass_1.ContextualClass) {
                css.addName(this._color.append('text'));
            }
            else {
                css.addName(this._color);
            }
        }
        css.addName(this.aligns.xs);
        css.addName(this._truncate);
        css.addName(this.fontWeight);
        css.addName(this.size);
        css.addName(this.style);
        return css;
    };
    TextCss.prototype.toString = function () {
        return this.cssClass().toString();
    };
    return TextCss;
}());
exports.TextCss = TextCss;
//# sourceMappingURL=TextCss.js.map