"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.DropdownBindingHandler = void 0;
var ko = require("knockout");
var bootstrap_1 = require("bootstrap");
var $ = require("jquery");
var DropdownBindingHandler = /** @class */ (function () {
    function DropdownBindingHandler() {
    }
    DropdownBindingHandler.prototype.init = function (element) {
        var $el = $(element);
        var $parentEl = $el.parent();
        var $menuEl = $parentEl.find('ul');
        if (!$menuEl.hasClass('dropdown-menu')) {
            $menuEl.addClass('dropdown-menu');
        }
        var dropdown = new bootstrap_1.Dropdown(element);
        $el.data('bs.dropdown', dropdown);
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            var d = dropdown;
            if (d) {
                d.dispose();
            }
            dropdown = null;
            $el = null;
        });
    };
    return DropdownBindingHandler;
}());
exports.DropdownBindingHandler = DropdownBindingHandler;
//# sourceMappingURL=DropdownBindingHandler.js.map