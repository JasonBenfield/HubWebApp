"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Heading2 = void 0;
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var Heading2ViewModel_1 = require("./Heading2ViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Heading2 = /** @class */ (function (_super) {
    tslib_1.__extends(Heading2, _super);
    function Heading2(vm) {
        if (vm === void 0) { vm = new Heading2ViewModel_1.Heading2ViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Heading2;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Heading2 = Heading2;
//# sourceMappingURL=Heading2.js.map