"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FormComponentViewModel = void 0;
var tslib_1 = require("tslib");
var HtmlComponentViewModel_1 = require("./HtmlComponentViewModel");
var template = require("./FormComponent.html");
var ComponentTemplate_1 = require("../ComponentTemplate");
var AggregateComponentViewModel_1 = require("./AggregateComponentViewModel");
var ko = require("knockout");
var Events_1 = require("../Events");
var DelayedAction_1 = require("../DelayedAction");
var FormComponentViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(FormComponentViewModel, _super);
    function FormComponentViewModel(content) {
        if (content === void 0) { content = new AggregateComponentViewModel_1.AggregateComponentViewModel(); }
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('form-component', template)) || this;
        _this.content = content;
        _this.action = ko.observable(null);
        _this.method = ko.observable(null);
        _this._submitted = new Events_1.SimpleEvent(_this);
        _this.submitted = _this._submitted.handler();
        _this.isDefaultSubmit = false;
        return _this;
    }
    FormComponentViewModel.prototype.useDefaultSubmit = function () {
        this.isDefaultSubmit = true;
    };
    FormComponentViewModel.prototype.submit = function (_, event) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (document.activeElement instanceof HTMLElement) {
                            document.activeElement.blur();
                        }
                        return [4 /*yield*/, DelayedAction_1.DelayedAction.delay(300)];
                    case 1:
                        _a.sent();
                        this._submitted.invoke();
                        return [2 /*return*/, this.isDefaultSubmit];
                }
            });
        });
    };
    return FormComponentViewModel;
}(HtmlComponentViewModel_1.HtmlComponentViewModel));
exports.FormComponentViewModel = FormComponentViewModel;
//# sourceMappingURL=FormComponentViewModel.js.map