"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SingleActivePanel = void 0;
var Panel_1 = require("./Panel");
var SingleActivePanel = /** @class */ (function () {
    function SingleActivePanel() {
        this.panels = [];
        this._previousActive = null;
        this._currentActive = null;
    }
    SingleActivePanel.prototype.add = function (panelVM, createContent) {
        var content = createContent(panelVM.content);
        var panel = new Panel_1.Panel(content, panelVM);
        this.panels.push(panel);
        return panel;
    };
    Object.defineProperty(SingleActivePanel.prototype, "previousActive", {
        get: function () { return this._previousActive; },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(SingleActivePanel.prototype, "currentActive", {
        get: function () { return this._currentActive; },
        enumerable: false,
        configurable: true
    });
    SingleActivePanel.prototype.activate = function (panel) {
        this._previousActive = this._currentActive;
        for (var _i = 0, _a = this.panels; _i < _a.length; _i++) {
            var p = _a[_i];
            p.deactivate();
        }
        panel.activate();
        this._currentActive = panel;
    };
    return SingleActivePanel;
}());
exports.SingleActivePanel = SingleActivePanel;
//# sourceMappingURL=SingleActivePanel.js.map