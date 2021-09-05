"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Container = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("./Block");
var BlockViewModel_1 = require("./BlockViewModel");
var Container = /** @class */ (function (_super) {
    tslib_1.__extends(Container, _super);
    function Container(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.containerCss = '';
        _this.setContainerCss('container');
        return _this;
    }
    Container.prototype.fluid = function () {
        this.setContainerCss('container-fluid');
    };
    Container.prototype.setContainerCss = function (containerCss) {
        this.removeCssName(this.containerCss);
        this.addCssName(containerCss);
        this.containerCss = containerCss;
    };
    return Container;
}(Block_1.Block));
exports.Container = Container;
//# sourceMappingURL=Container.js.map