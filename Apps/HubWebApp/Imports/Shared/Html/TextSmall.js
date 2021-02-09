"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Small_1 = require("./Small");
var SmallViewModel_1 = require("./SmallViewModel");
var TextSpan_1 = require("./TextSpan");
var TextSmall = /** @class */ (function (_super) {
    tslib_1.__extends(TextSmall, _super);
    function TextSmall(text, vm) {
        if (text === void 0) { text = ''; }
        if (vm === void 0) { vm = new SmallViewModel_1.SmallViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.textSpan = _this.addContent(new TextSpan_1.TextSpan());
        _this.setText(text);
        return _this;
    }
    TextSmall.prototype.setText = function (text) {
        this.textSpan.setText(text);
    };
    return TextSmall;
}(Small_1.Small));
exports.TextSmall = TextSmall;
//# sourceMappingURL=TextSmall.js.map