"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TextSpan = void 0;
var tslib_1 = require("tslib");
var TextSpanViewModel_1 = require("./TextSpanViewModel");
var HtmlComponent_1 = require("./HtmlComponent");
var TextSpan = /** @class */ (function (_super) {
    tslib_1.__extends(TextSpan, _super);
    function TextSpan(text, vm) {
        if (text === void 0) { text = ''; }
        if (vm === void 0) { vm = new TextSpanViewModel_1.TextSpanViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.setText(text);
        return _this;
    }
    TextSpan.prototype.setText = function (text) {
        this.text = text;
        this.vm.text(text);
    };
    TextSpan.prototype.setTitleFromText = function () {
        this.vm.title(this.text);
    };
    return TextSpan;
}(HtmlComponent_1.HtmlComponent));
exports.TextSpan = TextSpan;
//# sourceMappingURL=TextSpan.js.map