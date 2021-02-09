"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var Heading6ViewModel_1 = require("./Heading6ViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Heading6 = /** @class */ (function (_super) {
    tslib_1.__extends(Heading6, _super);
    function Heading6(vm) {
        if (vm === void 0) { vm = new Heading6ViewModel_1.Heading6ViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Heading6;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Heading6 = Heading6;
//# sourceMappingURL=Heading6.js.map