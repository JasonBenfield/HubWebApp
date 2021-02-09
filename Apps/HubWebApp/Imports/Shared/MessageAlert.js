"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var _ = require("lodash");
var ContextualClass_1 = require("./ContextualClass");
var Events_1 = require("./Events");
var BlockViewModel_1 = require("./Html/BlockViewModel");
var HtmlComponent_1 = require("./Html/HtmlComponent");
var TextBlock_1 = require("./Html/TextBlock");
var Alert_1 = require("./Alert");
var MessageAlert = /** @class */ (function (_super) {
    tslib_1.__extends(MessageAlert, _super);
    function MessageAlert(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.alert = new Alert_1.Alert(_this.vm);
        _this.textBlock = new TextBlock_1.TextBlock().addToContainer(_this.alert.content);
        _this._messageChanged = new Events_1.DefaultEvent(_this);
        _this.messageChanged = _this._messageChanged.handler();
        _this.debouncedSetMessage = _.debounce(function (message) {
            _this.updateVmMessage(message);
        }, 500);
        _this.addCssName('alert');
        return _this;
    }
    Object.defineProperty(MessageAlert.prototype, "message", {
        get: function () {
            return this._message;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(MessageAlert.prototype, "hasMessage", {
        get: function () {
            return Boolean(this._message);
        },
        enumerable: true,
        configurable: true
    });
    MessageAlert.prototype.clear = function () {
        this.setMessage('');
    };
    MessageAlert.prototype.success = function (message) {
        this.alert.setContext(ContextualClass_1.ContextualClass.success);
        this.setMessage(message);
    };
    MessageAlert.prototype.info = function (message) {
        this.alert.setContext(ContextualClass_1.ContextualClass.info);
        this.setMessage(message);
    };
    MessageAlert.prototype.infoAction = function (message, a) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.info(message);
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, , 3, 4]);
                        return [4 /*yield*/, a()];
                    case 2:
                        _a.sent();
                        return [3 /*break*/, 4];
                    case 3:
                        this.clear();
                        return [7 /*endfinally*/];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    MessageAlert.prototype.warning = function (message) {
        this.alert.setContext(ContextualClass_1.ContextualClass.warning);
        this.setMessage(message);
    };
    MessageAlert.prototype.danger = function (message) {
        this.alert.setContext(ContextualClass_1.ContextualClass.danger);
        this.setMessage(message);
    };
    MessageAlert.prototype.setMessage = function (message) {
        this._message = _.trim(message);
        this._messageChanged.invoke(this._message);
        if (this._message) {
            this.updateVmMessage(this._message);
        }
        this.debouncedSetMessage(this._message);
    };
    MessageAlert.prototype.updateVmMessage = function (message) {
        this.textBlock.setText(message);
        if (message) {
            this.show();
        }
        else {
            this.hide();
        }
    };
    return MessageAlert;
}(HtmlComponent_1.HtmlComponent));
exports.MessageAlert = MessageAlert;
//# sourceMappingURL=MessageAlert.js.map