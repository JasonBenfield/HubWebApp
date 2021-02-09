"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var HtmlComponent_1 = require("./HtmlComponent");
var TextBlockViewModel_1 = require("./TextBlockViewModel");
var TextBlock = /** @class */ (function (_super) {
    tslib_1.__extends(TextBlock, _super);
    function TextBlock(text, vm) {
        if (text === void 0) { text = ''; }
        if (vm === void 0) { vm = new TextBlockViewModel_1.TextBlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.setText(text);
        return _this;
    }
    TextBlock.prototype.setText = function (text) {
        this.text = text;
        this.vm.text(text);
    };
    TextBlock.prototype.setTitleFromText = function () {
        this.vm.title(this.text);
    };
    return TextBlock;
}(HtmlComponent_1.HtmlComponent));
exports.TextBlock = TextBlock;
//# sourceMappingURL=TextBlock.js.map