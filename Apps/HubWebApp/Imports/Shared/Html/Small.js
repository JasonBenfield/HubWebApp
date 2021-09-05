"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Small = void 0;
var tslib_1 = require("tslib");
var AggregateComponent_1 = require("./AggregateComponent");
var HtmlComponent_1 = require("./HtmlComponent");
var SmallViewModel_1 = require("./SmallViewModel");
var Small = /** @class */ (function (_super) {
    tslib_1.__extends(Small, _super);
    function Small(vm) {
        if (vm === void 0) { vm = new SmallViewModel_1.SmallViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.content = new AggregateComponent_1.AggregateComponent(_this.vm.content);
        return _this;
    }
    Small.prototype.addContent = function (item) {
        return item.addToContainer(this.content);
    };
    return Small;
}(HtmlComponent_1.HtmlComponent));
exports.Small = Small;
//# sourceMappingURL=Small.js.map