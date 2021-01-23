"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Panel = void 0;
var Panel = /** @class */ (function () {
    function Panel(content, vm) {
        this.content = content;
        this.vm = vm;
        this.isActive = false;
    }
    Panel.prototype.activate = function () {
        if (!this.isActive) {
            this.isActive = true;
            this.vm.isActive(this.isActive);
        }
    };
    Panel.prototype.deactivate = function () {
        if (this.isActive) {
            this.isActive = false;
            this.vm.isActive(this.isActive);
        }
    };
    return Panel;
}());
exports.Panel = Panel;
//# sourceMappingURL=Panel.js.map