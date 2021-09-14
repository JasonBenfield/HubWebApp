"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ButtonListGroupItem = void 0;
var tslib_1 = require("tslib");
var Button_1 = require("../Html/Button");
var ListGroupItem_1 = require("./ListGroupItem");
var ButtonListGroupItem = /** @class */ (function (_super) {
    tslib_1.__extends(ButtonListGroupItem, _super);
    function ButtonListGroupItem(vm) {
        var _this = _super.call(this, vm) || this;
        var css = vm.css();
        _this.button = new Button_1.Button(vm);
        _this.button.clearCss();
        _this.button.addCssName(css);
        _this.clicked = _this.button.clicked;
        return _this;
    }
    ButtonListGroupItem.prototype.enable = function () {
        this.button.enable();
    };
    ButtonListGroupItem.prototype.disable = function () {
        this.button.disable();
    };
    return ButtonListGroupItem;
}(ListGroupItem_1.ListGroupItem));
exports.ButtonListGroupItem = ButtonListGroupItem;
//# sourceMappingURL=ButtonListGroupItem.js.map