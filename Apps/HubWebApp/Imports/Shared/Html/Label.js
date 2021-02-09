"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var HtmlComponent_1 = require("./HtmlComponent");
var AggregateComponent_1 = require("./AggregateComponent");
var LabelViewModel_1 = require("./LabelViewModel");
var Label = /** @class */ (function (_super) {
    tslib_1.__extends(Label, _super);
    function Label(vm) {
        if (vm === void 0) { vm = new LabelViewModel_1.LabelViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.content = new AggregateComponent_1.AggregateComponent(_this.vm.content);
        return _this;
    }
    Label.prototype.setFor = function (forTarget) {
        this.vm.forTarget(forTarget);
    };
    Label.prototype.addContent = function (item) {
        return item.addToContainer(this.content);
    };
    return Label;
}(HtmlComponent_1.HtmlComponent));
exports.Label = Label;
//# sourceMappingURL=Label.js.map