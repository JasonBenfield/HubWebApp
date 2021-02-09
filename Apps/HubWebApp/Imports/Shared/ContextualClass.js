"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ContextualClass = /** @class */ (function () {
    function ContextualClass(value) {
        this.value = value;
    }
    Object.defineProperty(ContextualClass.prototype, "isDefault", {
        get: function () { return this.equals(ContextualClass.default); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isSuccess", {
        get: function () { return this.equals(ContextualClass.success); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isInfo", {
        get: function () { return this.equals(ContextualClass.info); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isWarning", {
        get: function () { return this.equals(ContextualClass.warning); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isDanger", {
        get: function () { return this.equals(ContextualClass.danger); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isPrimary", {
        get: function () { return this.equals(ContextualClass.primary); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isSecondary", {
        get: function () { return this.equals(ContextualClass.secondary); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isLight", {
        get: function () { return this.equals(ContextualClass.light); },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(ContextualClass.prototype, "isDark", {
        get: function () { return this.equals(ContextualClass.dark); },
        enumerable: true,
        configurable: true
    });
    ContextualClass.prototype.append = function (prefix) {
        var dash = prefix.lastIndexOf('-') === prefix.length - 1 ? '' : '-';
        return "" + prefix + dash + this.value;
    };
    ContextualClass.prototype.equals = function (other) {
        if (other) {
            if (typeof other === 'string') {
                return this.value === other;
            }
            return this.value === other.value;
        }
        return false;
    };
    ContextualClass.prototype.toString = function () {
        return this.value;
    };
    ContextualClass.default = new ContextualClass('default');
    ContextualClass.success = new ContextualClass('success');
    ContextualClass.info = new ContextualClass('info');
    ContextualClass.warning = new ContextualClass('warning');
    ContextualClass.danger = new ContextualClass('danger');
    ContextualClass.primary = new ContextualClass('primary');
    ContextualClass.secondary = new ContextualClass('secondary');
    ContextualClass.light = new ContextualClass('light');
    ContextualClass.dark = new ContextualClass('dark');
    return ContextualClass;
}());
exports.ContextualClass = ContextualClass;
//# sourceMappingURL=ContextualClass.js.map