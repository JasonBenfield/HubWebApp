"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Row = void 0;
var tslib_1 = require("tslib");
var FaIcon_1 = require("../FaIcon");
var Block_1 = require("../Html/Block");
var BlockViewModel_1 = require("../Html/BlockViewModel");
var HtmlComponent_1 = require("../Html/HtmlComponent");
var TextBlock_1 = require("../Html/TextBlock");
var Column_1 = require("./Column");
var LabelColumn_1 = require("./LabelColumn");
var Row = /** @class */ (function (_super) {
    tslib_1.__extends(Row, _super);
    function Row(vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.block = new Block_1.Block(_this.vm);
        _this.columns = [];
        _this.addCssName('row');
        return _this;
    }
    Row.prototype.addIconColumn = function (name, config) {
        var column = this.addColumn();
        var icon = column.addContent(new FaIcon_1.FaIcon(name));
        config && config(icon);
        return column;
    };
    Row.prototype.addTextColumn = function (text) {
        if (text === void 0) { text = ''; }
        var column = this.addColumn();
        column.addContent(new TextBlock_1.TextBlock(text));
        return column;
    };
    Row.prototype.addColumn = function () {
        var column = this.block.addContent(new Column_1.Column());
        this.columns.push(column);
        return column;
    };
    Row.prototype.addLabelColumn = function () {
        var column = this.block.addContent(new LabelColumn_1.LabelColumn());
        this.columns.push(column);
        return column;
    };
    return Row;
}(HtmlComponent_1.HtmlComponent));
exports.Row = Row;
//# sourceMappingURL=Row.js.map