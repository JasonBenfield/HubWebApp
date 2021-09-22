"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Block = void 0;
var tslib_1 = require("tslib");
var BlockViewModel_1 = require("./BlockViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Block = /** @class */ (function (_super) {
    tslib_1.__extends(Block, _super);
    function Block(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        return _super.call(this, vm) || this;
    }
    Block.prototype.height100 = function () {
        this.addCssName('h-100');
    };
    Block.prototype.flexFill = function () {
        this.addCssName('flex-fill');
    };
    Block.prototype.positionRelative = function () {
        this.addCssName('position-relative');
    };
    Block.prototype.positionAbsoluteFill = function () {
        this.addCssName('position-absolute-fill');
    };
    Block.prototype.scrollable = function () {
        this.addCssName('overflow-auto');
    };
    return Block;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Block = Block;
//# sourceMappingURL=Block.js.map