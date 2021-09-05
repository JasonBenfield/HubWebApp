"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CurrentVersionComponent = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var Row_1 = require("XtiShared/Grid/Row");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var CurrentVersionComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(CurrentVersionComponent, _super);
    function CurrentVersionComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Version');
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
    CurrentVersionComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var currentVersion;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getCurrentVersion()];
                    case 1:
                        currentVersion = _a.sent();
                        this.versionKey.setText(currentVersion.VersionKey);
                        this.version.setText(currentVersion.Major + "." + currentVersion.Minor + "." + currentVersion.Patch);
                        return [2 /*return*/];
                }
            });
        });
    };
    CurrentVersionComponent.prototype.getCurrentVersion = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var currentVersion;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.App.GetCurrentVersion()];
                                    case 1:
                                        currentVersion = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, currentVersion];
                }
            });
        });
    };
    return CurrentVersionComponent;
}(Card_1.Card));
exports.CurrentVersionComponent = CurrentVersionComponent;
//# sourceMappingURL=CurrentVersionComponent.js.map