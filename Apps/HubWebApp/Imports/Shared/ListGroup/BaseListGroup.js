"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BaseListGroup = void 0;
var tslib_1 = require("tslib");
var BaseList_1 = require("../Html/BaseList");
var BaseListGroup = /** @class */ (function (_super) {
    tslib_1.__extends(BaseListGroup, _super);
    function BaseListGroup(createItem, createItemVM, vm) {
        var _this = _super.call(this, createItem, createItemVM, vm) || this;
        _this.addCssName('list-group');
        return _this;
    }
    BaseListGroup.prototype.makeFlush = function () {
        this.addCssName('list-group-flush');
    };
    return BaseListGroup;
}(BaseList_1.BaseList));
exports.BaseListGroup = BaseListGroup;
//# sourceMappingURL=BaseListGroup.js.map