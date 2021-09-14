"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.InputGroup = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("./Block");
var BlockViewModel_1 = require("./BlockViewModel");
var Input_1 = require("./Input");
var InputViewModel_1 = require("./InputViewModel");
var InputGroup = /** @class */ (function (_super) {
    tslib_1.__extends(InputGroup, _super);
    function InputGroup(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addCssName('input-group');
        return _this;
    }
    InputGroup.prototype.addInput = function (vm) {
        if (vm === void 0) { vm = new InputViewModel_1.InputViewModel(); }
        return this.addContent(new Input_1.Input(vm));
    };
    return InputGroup;
}(Block_1.Block));
exports.InputGroup = InputGroup;
//# sourceMappingURL=InputGroup.js.map