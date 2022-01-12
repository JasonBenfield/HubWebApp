"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListItemView = void 0;
var tslib_1 = require("tslib");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var ResourceListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceListItemView, _super);
    function ResourceListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.resourceName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs(8)); })
            .addContent(new TextSpanView_1.TextSpanView());
        _this.resultType = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return ResourceListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.ResourceListItemView = ResourceListItemView;
//# sourceMappingURL=ResourceListItemView.js.map