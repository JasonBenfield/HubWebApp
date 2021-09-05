"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Heading3 = void 0;
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var Heading3ViewModel_1 = require("./Heading3ViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Heading3 = /** @class */ (function (_super) {
    tslib_1.__extends(Heading3, _super);
    function Heading3(vm) {
        if (vm === void 0) { vm = new Heading3ViewModel_1.Heading3ViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Heading3;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Heading3 = Heading3;
//# sourceMappingURL=Heading3.js.map