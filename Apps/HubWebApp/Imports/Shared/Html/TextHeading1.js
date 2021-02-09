"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Heading1_1 = require("./Heading1");
var Heading1ViewModel_1 = require("./Heading1ViewModel");
var TextSpan_1 = require("./TextSpan");
var TextHeading1 = /** @class */ (function (_super) {
    tslib_1.__extends(TextHeading1, _super);
    function TextHeading1(text, vm) {
        if (text === void 0) { text = ''; }
        if (vm === void 0) { vm = new Heading1ViewModel_1.Heading1ViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.textSpan = _this.addContent(new TextSpan_1.TextSpan());
        _this.setText(text);
        return _this;
    }
    TextHeading1.prototype.setText = function (text) {
        this.textSpan.setText(text);
    };
    return TextHeading1;
}(Heading1_1.Heading1));
exports.TextHeading1 = TextHeading1;
//# sourceMappingURL=TextHeading1.js.map