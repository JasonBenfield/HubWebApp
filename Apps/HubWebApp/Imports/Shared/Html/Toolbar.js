"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var tslib_1 = require("tslib");
var ColumnCss_1 = require("../ColumnCss");
var Row_1 = require("../Grid/Row");
var Block_1 = require("./Block");
var BlockViewModel_1 = require("./BlockViewModel");
var Container_1 = require("./Container");
var HtmlComponent_1 = require("./HtmlComponent");
var Toolbar = /** @class */ (function (_super) {
    tslib_1.__extends(Toolbar, _super);
    function Toolbar(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        var container = new Block_1.Block(vm).addContent(new Container_1.Container());
        var row = container.addContent(new Row_1.Row());
        _this.columnStart = row.addColumn()
            .configure(function (c) {
            c.setColumnCss(ColumnCss_1.ColumnCss.xs());
        });
        _this.columnEnd = row.addColumn()
            .configure(function (c) {
            c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto'));
        });
        return _this;
    }
    return Toolbar;
}(HtmlComponent_1.HtmlComponent));
exports.Toolbar = Toolbar;
//# sourceMappingURL=Toolbar.js.map