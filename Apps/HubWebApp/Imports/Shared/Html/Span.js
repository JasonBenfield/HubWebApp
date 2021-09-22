"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Span = void 0;
var tslib_1 = require("tslib");
var SpanViewModel_1 = require("./SpanViewModel");
var AggregateComponent_1 = require("./AggregateComponent");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var Span = /** @class */ (function (_super) {
    tslib_1.__extends(Span, _super);
    function Span(vm) {
        if (vm === void 0) { vm = new SpanViewModel_1.SpanViewModel(); }
        return _super.call(this, vm, new AggregateComponent_1.AggregateComponent(vm.content)) || this;
    }
    return Span;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.Span = Span;
//# sourceMappingURL=Span.js.map