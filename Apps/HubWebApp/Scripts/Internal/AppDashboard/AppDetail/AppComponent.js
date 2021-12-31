"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponent = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var AppComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppComponent, _super);
    function AppComponent(hubApi, view) {
        var _this = _super.call(this) || this;
        _this.hubApi = hubApi;
        new TextBlock_1.TextBlock('App', view.titleHeader);
        _this.alert = new MessageAlert_1.MessageAlert(view.alert);
        _this.appName = new TextBlock_1.TextBlock('', view.appName);
        _this.appTitle = new TextBlock_1.TextBlock('', view.appTitle);
        _this.appType = new TextBlock_1.TextBlock('', view.appType);
        return _this;
    }
    AppComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var app;
            return (0, tslib_1.__generator)(this, function (_a) {
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
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var app;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
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
}(CardView_1.CardView));
exports.AppComponent = AppComponent;
//# sourceMappingURL=AppComponent.js.map