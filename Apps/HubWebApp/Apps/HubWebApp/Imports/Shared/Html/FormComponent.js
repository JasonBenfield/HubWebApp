"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FormComponent = void 0;
var tslib_1 = require("tslib");
var ButtonCommandItem_1 = require("../Command/ButtonCommandItem");
var AggregateComponent_1 = require("./AggregateComponent");
var FormComponentViewModel_1 = require("./FormComponentViewModel");
var HtmlContainerComponent_1 = require("./HtmlContainerComponent");
var FormComponent = /** @class */ (function (_super) {
    tslib_1.__extends(FormComponent, _super);
    function FormComponent(vm) {
        if (vm === void 0) { vm = new FormComponentViewModel_1.FormComponentViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.submitted = _this.vm.submitted;
        _this.content = new AggregateComponent_1.AggregateComponent(_this.vm.content);
        _this.setAction("#");
        return _this;
    }
    FormComponent.prototype.useDefaultSubmit = function () {
        this.vm.useDefaultSubmit();
    };
    FormComponent.prototype.setAction = function (action) { this.vm.action(action); };
    FormComponent.prototype.setMethod = function (method) { this.vm.method(method); };
    FormComponent.prototype.addOffscreenSubmit = function () {
        return this.addContent(new ButtonCommandItem_1.ButtonCommandItem())
            .configure(function (button) {
            button.makeOffscreenSubmit();
        });
    };
    return FormComponent;
}(HtmlContainerComponent_1.HtmlContainerComponent));
exports.FormComponent = FormComponent;
//# sourceMappingURL=FormComponent.js.map