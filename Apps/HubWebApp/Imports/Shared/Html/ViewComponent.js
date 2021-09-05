"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ViewComponent = void 0;
var ViewComponent = /** @class */ (function () {
    function ViewComponent(view) {
        this.view = view;
    }
    ViewComponent.prototype.addToContainer = function (container) {
        this.view.addToContainer(container);
        return this;
    };
    ViewComponent.prototype.insertIntoContainer = function (container, index) {
        this.view.insertIntoContainer(container, index);
        return this;
    };
    ViewComponent.prototype.removeFromContainer = function (container) {
        this.view.removeFromContainer(container);
        return this;
    };
    return ViewComponent;
}());
exports.ViewComponent = ViewComponent;
//# sourceMappingURL=ViewComponent.js.map