"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var Heading1ViewModel_1 = require("./Heading1ViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Heading1 = /** @class */ (function (_super) {
    tslib_1.__extends(Heading1, _super);
    function Heading1(vm) {
        if (vm === void 0) { vm = new Heading1ViewModel_1.Heading1ViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Heading1;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Heading1 = Heading1;
//# sourceMappingURL=Heading1.js.map