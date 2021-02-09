"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ColumnCss_1 = require("../ColumnCss");
var Block_1 = require("../Html/Block");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var Column = /** @class */ (function (_super) {
    tslib_1.__extends(Column, _super);
    function Column(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.setColumnCss(ColumnCss_1.ColumnCss.xs());
        return _this;
    }
    Column.prototype.setColumnCss = function (columnCss) {
        this.replaceCssName(this.columnCss && this.columnCss.toString(), columnCss && columnCss.toString());
        this.columnCss = columnCss;
    };
    return Column;
}(Block_1.Block));
exports.Column = Column;
//# sourceMappingURL=Column.js.map