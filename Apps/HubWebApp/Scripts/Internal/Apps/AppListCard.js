"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListCard = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var Events_1 = require("XtiShared/Events");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var AppListCard = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppListCard, _super);
    function AppListCard(hubApi, appRedirectUrl, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.appRedirectUrl = appRedirectUrl;
        _this._appSelected = new Events_1.DefaultEvent(_this);
        _this.appSelected = _this._appSelected.handler();
        _this.addCardTitleHeader('Apps');
        _this.alert = _this.addCardAlert().alert;
        _this.apps = _this.addLinkListGroup();
        _this.apps.itemClicked.register(_this.onAppSelected.bind(_this));
        return _this;
    }
    AppListCard.prototype.onAppSelected = function (listItem) {
        this._appSelected.invoke(listItem.getData());
    };
    AppListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var apps;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getApps()];
                    case 1:
                        apps = _a.sent();
                        this.apps.setItems(apps, function (sourceItem, listItem) {
                            var row = listItem.addContent(new Row_1.Row());
                            row.addColumn()
                                .addContent(new TextSpan_1.TextSpan(sourceItem.AppName));
                            row.addColumn()
                                .addContent(new TextSpan_1.TextSpan(sourceItem.Title));
                            row.addColumn()
                                .addContent(new TextSpan_1.TextSpan(sourceItem.Type.DisplayText));
                            listItem.setHref(_this.appRedirectUrl(sourceItem.ID));
                        });
                        if (apps.length === 0) {
                            this.alert.danger('No Apps were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    AppListCard.prototype.getApps = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var apps;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Apps.All()];
                                    case 1:
                                        apps = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, apps];
                }
            });
        });
    };
    return AppListCard;
}(Card_1.Card));
exports.AppListCard = AppListCard;
//# sourceMappingURL=AppListCard.js.map