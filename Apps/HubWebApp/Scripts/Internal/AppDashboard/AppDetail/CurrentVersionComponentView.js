"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CurrentVersionComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var CurrentVersionComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(CurrentVersionComponentView, _super);
    function CurrentVersionComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        var row = _this.addCardBody()
            .addContent(new Row_1.Row());
        _this.versionKey = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpan_1.TextSpan());
        _this.version = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        return _this;
    }
    CurrentVersionComponentView.prototype.setVersionKey = function (versionKey) { this.versionKey.setText(versionKey); };
    CurrentVersionComponentView.prototype.setVersion = function (version) { this.version.setText(version); };
    return CurrentVersionComponentView;
}(CardView_1.CardView));
exports.CurrentVersionComponentView = CurrentVersionComponentView;
//# sourceMappingURL=CurrentVersionComponentView.js.map