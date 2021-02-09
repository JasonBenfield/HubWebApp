"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Block_1 = require("../Html/Block");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var CardHeader = /** @class */ (function (_super) {
    tslib_1.__extends(CardHeader, _super);
    function CardHeader(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.vm.css('card-header');
        return _this;
    }
    return CardHeader;
}(Block_1.Block));
exports.CardHeader = CardHeader;
//# sourceMappingURL=CardHeader.js.map