"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LabelColumn = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("../ColumnCss");
var Label_1 = require("../Html/Label");
var LabelViewModel_1 = require("../Html/LabelViewModel");
var LabelColumn = /** @class */ (function (_super) {
    tslib_1.__extends(LabelColumn, _super);
    function LabelColumn(vm) {
        if (vm === void 0) { vm = new LabelViewModel_1.LabelViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.setColumnCss(ColumnCss_1.ColumnCss.xs());
        _this.addCssName('col-form-label');
        return _this;
    }
    LabelColumn.prototype.setColumnCss = function (columnCss) {
        this.replaceCssName(this.columnCss && this.columnCss.toString(), columnCss && columnCss.toString());
    };
    return LabelColumn;
}(Label_1.Label));
exports.LabelColumn = LabelColumn;
//# sourceMappingURL=LabelColumn.js.map