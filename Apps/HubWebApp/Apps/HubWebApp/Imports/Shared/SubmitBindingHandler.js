"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SubmitBindingHandler = void 0;
var tslib_1 = require("tslib");
var ko = require("knockout");
var _ = require("lodash");
var DelayedAction_1 = require("./DelayedAction");
var SubmitBindingHandler = /** @class */ (function () {
    function SubmitBindingHandler() {
        this.init = this.init.bind(this);
    }
    SubmitBindingHandler.prototype.init = function (element, valueAccessor) {
        var _this = this;
        ko.utils.registerEventHandler(element, "submit", function (event) { return tslib_1.__awaiter(_this, void 0, void 0, function () {
            var unwrapped, model, result;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        unwrapped = ko.utils.unwrapObservable(valueAccessor());
                        if (!unwrapped.requestExecute) return [3 /*break*/, 2];
                        if (document.activeElement instanceof HTMLElement) {
                            document.activeElement.blur();
                        }
                        return [4 /*yield*/, DelayedAction_1.DelayedAction.delay(300)];
                    case 1:
                        _a.sent();
                        unwrapped.requestExecute.call(unwrapped, element);
                        if (event.preventDefault) {
                            event.preventDefault();
                        }
                        else {
                            event.returnValue = false;
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        if (_.isFunction(unwrapped)) {
                            model = ko.dataFor(element);
                            result = unwrapped.call(model, element);
                            if (result !== true) {
                                if (event.preventDefault) {
                                    event.preventDefault();
                                }
                                else {
                                    event.returnValue = false;
                                }
                            }
                        }
                        _a.label = 3;
                    case 3: return [2 /*return*/];
                }
            });
        }); });
    };
    return SubmitBindingHandler;
}());
exports.SubmitBindingHandler = SubmitBindingHandler;
//# sourceMappingURL=SubmitBindingHandler.js.map