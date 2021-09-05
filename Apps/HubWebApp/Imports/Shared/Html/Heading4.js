"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Heading4 = void 0;
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var Heading4ViewModel_1 = require("./Heading4ViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Heading4 = /** @class */ (function (_super) {
    tslib_1.__extends(Heading4, _super);
    function Heading4(vm) {
        if (vm === void 0) { vm = new Heading4ViewModel_1.Heading4ViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Heading4;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Heading4 = Heading4;
//# sourceMappingURL=Heading4.js.map