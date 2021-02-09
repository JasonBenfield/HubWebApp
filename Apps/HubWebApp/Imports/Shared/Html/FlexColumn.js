"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Block_1 = require("./Block");
var BlockViewModel_1 = require("./BlockViewModel");
var FlexColumn = /** @class */ (function (_super) {
    tslib_1.__extends(FlexColumn, _super);
    function FlexColumn(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addCssName('d-flex flex-column h-100');
        return _this;
    }
    return FlexColumn;
}(Block_1.Block));
exports.FlexColumn = FlexColumn;
//# sourceMappingURL=FlexColumn.js.map