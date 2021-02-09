"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponent = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var Row_1 = require("XtiShared/Grid/Row");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var AppComponent = /** @class */ (function (_super) {
    tslib_1.__extends(AppComponent, _super);
    function AppComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('App');
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
    AppComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var app;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getApp()];
                    case 1:
                        app = _a.sent();
                        this.appName.setText(app.AppName);
                        this.appTitle.setText(app.Title);
                        this.appType.setText(app.Type.DisplayText);
                        return [2 /*return*/];
                }
            });
        });
    };
    AppComponent.prototype.getApp = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var app;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetApp()];
                                    case 1:
                                        app = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, app];
                }
            });
        });
    };
    return AppComponent;
}(Card_1.Card));
exports.AppComponent = AppComponent;
//# sourceMappingURL=AppComponent.js.map