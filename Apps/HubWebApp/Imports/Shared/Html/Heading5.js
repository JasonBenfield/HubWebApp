"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var Heading5ViewModel_1 = require("./Heading5ViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Heading5 = /** @class */ (function (_super) {
    tslib_1.__extends(Heading5, _super);
    function Heading5(vm) {
        if (vm === void 0) { vm = new Heading5ViewModel_1.Heading5ViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Heading5;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Heading5 = Heading5;
//# sourceMappingURL=Heading5.js.map