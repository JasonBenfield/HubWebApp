"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Link_1 = require("./Link");
var LinkViewModel_1 = require("./LinkViewModel");
var TextSpan_1 = require("./TextSpan");
var TextLink = /** @class */ (function (_super) {
    tslib_1.__extends(TextLink, _super);
    function TextLink(text, vm) {
        if (text === void 0) { text = ''; }
        if (vm === void 0) { vm = new LinkViewModel_1.LinkViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.textSpan = _this.addContent(new TextSpan_1.TextSpan());
        _this.setText(text);
        return _this;
    }
    TextLink.prototype.setText = function (text) {
        this.textSpan.setText(text);
    };
    return TextLink;
}(Link_1.Link));
exports.TextLink = TextLink;
//# sourceMappingURL=TextLink.js.map