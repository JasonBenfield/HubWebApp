"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Panel = void 0;
var AggregateComponent_1 = require("XtiShared/Html/AggregateComponent");
var AggregateComponentViewModel_1 = require("XtiShared/Html/AggregateComponentViewModel");
var Panel = /** @class */ (function () {
    function Panel(content, vm) {
        if (vm === void 0) { vm = new AggregateComponentViewModel_1.AggregateComponentViewModel(); }
        this.content = content;
        this.vm = vm;
        this.isActive = true;
        this.container = new AggregateComponent_1.AggregateComponent(vm);
        this.container.addContent(content);
    }
    Panel.prototype.addToContainer = function (container) {
        return container.addItem(this.vm, this);
    };
    Panel.prototype.insertIntoContainer = function (container, index) {
        return container.insertItem(index, this.vm, this);
    };
    Panel.prototype.removeFromContainer = function (container) {
        return container.removeItem(this);
    };
    Panel.prototype.activate = function () {
        if (!this.isActive) {
            this.isActive = true;
            this.container.show();
        }
    };
    Panel.prototype.deactivate = function () {
        if (this.isActive) {
            this.isActive = false;
            this.container.hide();
        }
    };
    return Panel;
}());
exports.Panel = Panel;
//# sourceMappingURL=Panel.js.map