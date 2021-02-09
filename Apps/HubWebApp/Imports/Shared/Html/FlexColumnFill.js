"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ContextualClass_1 = require("../ContextualClass");
var Block_1 = require("./Block");
var BlockViewModel_1 = require("./BlockViewModel");
var Container_1 = require("./Container");
var HtmlComponent_1 = require("./HtmlComponent");
var FlexColumnFill = /** @class */ (function (_super) {
    tslib_1.__extends(FlexColumnFill, _super);
    function FlexColumnFill(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        var block = new Block_1.Block(vm);
        block.flexFill();
        _this.setBackgroundContext(ContextualClass_1.ContextualClass.light);
        block.positionRelative();
        var absFillBlock = block.addContent(new Block_1.Block());
        absFillBlock.positionAbsoluteFill();
        absFillBlock.scrollable();
        _this.container = absFillBlock.addContent(new Container_1.Container());
        return _this;
    }
    FlexColumnFill.prototype.addContent = function (item) {
        item.addToContainer(this.container);
        return item;
    };
    return FlexColumnFill;
}(HtmlComponent_1.HtmlComponent));
exports.FlexColumnFill = FlexColumnFill;
//# sourceMappingURL=FlexColumnFill.js.map