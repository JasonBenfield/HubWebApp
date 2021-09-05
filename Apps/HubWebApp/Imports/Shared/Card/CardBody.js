"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CardBody = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("../Html/Block");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var CardBody = /** @class */ (function (_super) {
    tslib_1.__extends(CardBody, _super);
    function CardBody(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.vm.css('card-body');
        return _this;
    }
    return CardBody;
}(Block_1.Block));
exports.CardBody = CardBody;
//# sourceMappingURL=CardBody.js.map