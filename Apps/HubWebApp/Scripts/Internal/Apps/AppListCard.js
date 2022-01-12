"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListCard = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var AppListItem_1 = require("./AppListItem");
var AppListCard = /** @class */ (function () {
    function AppListCard(hubApi, appRedirectUrl, view) {
        this.hubApi = hubApi;
        this.appRedirectUrl = appRedirectUrl;
        this.view = view;
        this._appSelected = new Events_1.DefaultEvent(this);
        this.appSelected = this._appSelected.handler();
        new TextBlock_1.TextBlock('Apps', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.apps = new ListGroup_1.ListGroup(this.view.apps);
        this.apps.itemClicked.register(this.onAppSelected.bind(this));
    }
    AppListCard.prototype.onAppSelected = function (listItem) {
        this._appSelected.invoke(listItem.appWithModKey.App);
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
                            return new AppListItem_1.AppListItem(sourceItem, _this.appRedirectUrl, listItem);
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
        var _this = this;
        return this.alert.infoAction('Loading...', function () { return _this.hubApi.Apps.GetApps(); });
    };
    return AppListCard;
}());
exports.AppListCard = AppListCard;
//# sourceMappingURL=AppListCard.js.map