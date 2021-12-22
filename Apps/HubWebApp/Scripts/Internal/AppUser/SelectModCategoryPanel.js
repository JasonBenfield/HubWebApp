"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SelectModCategoryPanel = exports.SelectModCategoryPanelResult = void 0;
var Awaitable_1 = require("@jasonbenfield/sharedwebapp/Awaitable");
var SelectModCategoryPanelResult = /** @class */ (function () {
    function SelectModCategoryPanelResult(results) {
        this.results = results;
    }
    SelectModCategoryPanelResult.defaultModSelected = function () {
        return new SelectModCategoryPanelResult({ defaultModSelected: {} });
    };
    SelectModCategoryPanelResult.modCategorySelected = function () {
        return new SelectModCategoryPanelResult({ modCategorySelected: {} });
    };
    Object.defineProperty(SelectModCategoryPanelResult.prototype, "defaultModSelected", {
        get: function () {
            return this.results.defaultModSelected;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(SelectModCategoryPanelResult.prototype, "modCategorySelected", {
        get: function () {
            return this.results.modCategorySelected;
        },
        enumerable: false,
        configurable: true
    });
    return SelectModCategoryPanelResult;
}());
exports.SelectModCategoryPanelResult = SelectModCategoryPanelResult;
var SelectModCategoryPanel = /** @class */ (function () {
    function SelectModCategoryPanel() {
        this.awaitable = new Awaitable_1.Awaitable();
    }
    SelectModCategoryPanel.prototype.start = function () {
        return this.awaitable.start();
    };
    return SelectModCategoryPanel;
}());
exports.SelectModCategoryPanel = SelectModCategoryPanel;
//# sourceMappingURL=SelectModCategoryPanel.js.map