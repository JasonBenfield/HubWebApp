"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var DropdownButton_1 = require("./DropdownButton");
var DropdownSpanItem_1 = require("./DropdownSpanItem");
var DropdownMenu_1 = require("./DropdownMenu");
var DropdownLinkItem_1 = require("./DropdownLinkItem");
var DropdownComponentViewModel_1 = require("./DropdownComponentViewModel");
var DropdownComponent = /** @class */ (function () {
    function DropdownComponent(vm) {
        if (vm === void 0) { vm = new DropdownComponentViewModel_1.DropdownComponentViewModel(); }
        this.vm = vm;
        this.button = new DropdownButton_1.DropdownButton(this.vm.button);
        this.menu = new DropdownMenu_1.DropdownMenu(null, null, this.vm.menu);
    }
    DropdownComponent.prototype.addToContainer = function (container) {
        return container.addItem(this.vm, this);
    };
    DropdownComponent.prototype.insertIntoContainer = function (container, index) {
        return container.insertItem(index, this.vm, this);
    };
    DropdownComponent.prototype.removeFromContainer = function (container) {
        return container.removeItem(this);
    };
    DropdownComponent.prototype.addSpanItem = function () {
        return new DropdownSpanItem_1.DropdownSpanItem().addToList(this.menu);
    };
    DropdownComponent.prototype.addLinkItem = function () {
        return new DropdownLinkItem_1.DropdownLinkItem().addToList(this.menu);
    };
    DropdownComponent.prototype.show = function () {
        this.vm.isVisible(true);
    };
    DropdownComponent.prototype.hide = function () {
        this.vm.isVisible(false);
    };
    return DropdownComponent;
}());
exports.DropdownComponent = DropdownComponent;
//# sourceMappingURL=DropdownComponent.js.map