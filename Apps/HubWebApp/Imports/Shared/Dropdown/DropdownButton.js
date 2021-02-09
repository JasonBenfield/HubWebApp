"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var Button_1 = require("../Html/Button");
var ButtonViewModel_1 = require("../Html/ButtonViewModel");
var DropdownButton = /** @class */ (function (_super) {
    tslib_1.__extends(DropdownButton, _super);
    function DropdownButton(vm) {
        if (vm === void 0) { vm = new ButtonViewModel_1.ButtonViewModel(); }
        var _this = _super.call(this, vm) || this;
        vm.type('button');
        _this.addCssName('dropdown-toggle');
        return _this;
    }
    return DropdownButton;
}(Button_1.Button));
exports.DropdownButton = DropdownButton;
//# sourceMappingURL=DropdownButton.js.map