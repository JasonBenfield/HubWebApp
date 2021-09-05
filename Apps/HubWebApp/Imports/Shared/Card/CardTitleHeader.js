"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CardTitleHeader = void 0;
var tslib_1 = require("tslib");
var CardHeader_1 = require("./CardHeader");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var TextBlock_1 = require("../Html/TextBlock");
var CardTitleHeader = /** @class */ (function (_super) {
    tslib_1.__extends(CardTitleHeader, _super);
    function CardTitleHeader(title, vm) {
        if (title === void 0) { title = ''; }
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.textBlock = _this.addContent(new TextBlock_1.TextBlock());
        _this.setText(title);
        return _this;
    }
    CardTitleHeader.prototype.setText = function (text) {
        this.textBlock.setText(text);
    };
    return CardTitleHeader;
}(CardHeader_1.CardHeader));
exports.CardTitleHeader = CardTitleHeader;
//# sourceMappingURL=CardTitleHeader.js.map