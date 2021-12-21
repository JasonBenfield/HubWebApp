"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var AppComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppComponentView, _super);
    function AppComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        var row = _this.addCardBody()
            .addContent(new Row_1.Row());
        _this.appName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpan_1.TextSpan());
        _this.appTitle = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpan_1.TextSpan());
        _this.appType = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        return _this;
    }
    AppComponentView.prototype.setAppName = function (appName) { this.appName.setText(appName); };
    AppComponentView.prototype.setAppTitle = function (appTitle) { this.appTitle.setText(appTitle); };
    AppComponentView.prototype.setAppType = function (appType) { this.appType.setText(appType); };
    return AppComponentView;
}(CardView_1.CardView));
exports.AppComponentView = AppComponentView;
//# sourceMappingURL=AppComponentView.js.map