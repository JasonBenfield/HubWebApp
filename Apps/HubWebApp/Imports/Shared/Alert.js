"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ContextualClass_1 = require("./ContextualClass");
var BlockViewModel_1 = require("./Html/BlockViewModel");
var Block_1 = require("./Html/Block");
var Alert = /** @class */ (function (_super) {
    tslib_1.__extends(Alert, _super);
    function Alert(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.context = ContextualClass_1.ContextualClass.info;
        _this.addCssName('alert');
        return _this;
    }
    Alert.prototype.setContext = function (context) {
        var css = this.context.append('alert');
        this.replaceCssName(css, context.append('alert'));
        this.context = context;
    };
    return Alert;
}(Block_1.Block));
exports.Alert = Alert;
//# sourceMappingURL=Alert.js.map