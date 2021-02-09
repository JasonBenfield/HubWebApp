"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ButtonCommandItem_1 = require("./ButtonCommandItem");
var AsyncCommand = /** @class */ (function () {
    function AsyncCommand(action) {
        this.action = action;
        this.items = [];
        this.isMultiExecutionAllowed = false;
        this.isEnabled = true;
        this.executionCount = 0;
        this.inProgressAnimation = null;
    }
    AsyncCommand.prototype.configure = function (action) {
        action(this);
        return this;
    };
    AsyncCommand.prototype.addButton = function (vm) {
        return this.add(new ButtonCommandItem_1.ButtonCommandItem(vm));
    };
    AsyncCommand.prototype.add = function (item) {
        var _this = this;
        this.items.push(item);
        item.executeRequested.register(function (context) { return _this.execute(context); });
        return item;
    };
    AsyncCommand.prototype.animateIconWhenInProgress = function (inProgressAnimation) {
        this.inProgressAnimation = inProgressAnimation;
    };
    AsyncCommand.prototype.icons = function (action) {
        this.forEachItem(function (c) {
            action(c.icon);
        });
    };
    AsyncCommand.prototype.forEachItem = function (action) {
        for (var _i = 0, _a = this.items; _i < _a.length; _i++) {
            var item = _a[_i];
            action(item);
        }
    };
    AsyncCommand.prototype.activate = function () {
        this.forEachItem(function (c) { return c.setActive(); });
    };
    AsyncCommand.prototype.deactivate = function () {
        this.forEachItem(function (c) { return c.setInactive(); });
    };
    AsyncCommand.prototype.allowMultiExecution = function () {
        this.isMultiExecutionAllowed = true;
    };
    AsyncCommand.prototype.show = function () {
        this.forEachItem(function (c) { return c.show(); });
    };
    AsyncCommand.prototype.hide = function () {
        this.forEachItem(function (c) { return c.hide(); });
    };
    AsyncCommand.prototype.enable = function () {
        this.isEnabled = true;
        this.updateIsEnabled();
    };
    AsyncCommand.prototype.disable = function () {
        this.updateIsEnabled();
    };
    AsyncCommand.prototype.updateIsEnabled = function () {
        var isEnabled = this.isEnabled && this.executionCount === 0;
        if (isEnabled) {
            this.forEachItem(function (c) { return c.enable(); });
        }
        else {
            this.forEachItem(function (c) { return c.disable(); });
        }
    };
    AsyncCommand.prototype.execute = function (context) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var canExecute;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        canExecute = this.canExecute();
                        if (!canExecute) return [3 /*break*/, 4];
                        this.executionCount++;
                        if (this.inProgressAnimation) {
                            this.icons(function (i) {
                                i.startAnimation(_this.inProgressAnimation);
                            });
                        }
                        this.updateIsEnabled();
                        _a.label = 1;
                    case 1:
                        _a.trys.push([1, , 3, 4]);
                        return [4 /*yield*/, this.action(context)];
                    case 2:
                        _a.sent();
                        return [3 /*break*/, 4];
                    case 3:
                        this.executionCount--;
                        this.updateIsEnabled();
                        if (this.inProgressAnimation) {
                            this.icons(function (i) {
                                i.stopAnimation();
                            });
                        }
                        return [7 /*endfinally*/];
                    case 4: return [2 /*return*/];
                }
            });
        });
    };
    AsyncCommand.prototype.canExecute = function () {
        return this.isEnabled && (this.isMultiExecutionAllowed || this.executionCount === 0);
    };
    return AsyncCommand;
}());
exports.AsyncCommand = AsyncCommand;
//# sourceMappingURL=AsyncCommand.js.map