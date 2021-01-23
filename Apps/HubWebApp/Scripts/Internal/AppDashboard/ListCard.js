"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ListCard = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var Enumerable_1 = require("XtiShared/Enumerable");
var ListCard = /** @class */ (function () {
    function ListCard(vm, noItemsFoundMessage) {
        this.vm = vm;
        this.noItemsFoundMessage = noItemsFoundMessage;
        this.alert = new Alert_1.Alert(this.vm.alert);
    }
    ListCard.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var sourceItems, items;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this._getSourceItems()];
                    case 1:
                        sourceItems = _a.sent();
                        items = new Enumerable_1.MappedArray(sourceItems, function (s) { return _this.createItem(s); }).value();
                        this.vm.items(items);
                        this.vm.hasItems(sourceItems.length > 0);
                        if (sourceItems.length === 0) {
                            this.alert.danger(this.noItemsFoundMessage);
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ListCard.prototype._getSourceItems = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var sourceItems;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        sourceItems = [];
                        return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                                return tslib_1.__generator(this, function (_a) {
                                    switch (_a.label) {
                                        case 0: return [4 /*yield*/, this.getSourceItems()];
                                        case 1:
                                            sourceItems = _a.sent();
                                            return [2 /*return*/];
                                    }
                                });
                            }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, sourceItems];
                }
            });
        });
    };
    return ListCard;
}());
exports.ListCard = ListCard;
//# sourceMappingURL=ListCard.js.map