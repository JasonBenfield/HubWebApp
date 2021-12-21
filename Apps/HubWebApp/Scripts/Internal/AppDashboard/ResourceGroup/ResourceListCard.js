"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListCard = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var ResourceListItem_1 = require("./ResourceListItem");
var ResourceListCard = /** @class */ (function () {
    function ResourceListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._resourceSelected = new Events_1.DefaultEvent(this);
        this.resourceSelected = this._resourceSelected.handler();
        new CardTitleHeader_1.CardTitleHeader('Resources', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.resources = new ListGroup_1.ListGroup(this.view.resources);
        this.resources.itemClicked.register(this.onItemSelected.bind(this));
    }
    ResourceListCard.prototype.onItemSelected = function (item) {
        this._resourceSelected.invoke(item.resource);
    };
    ResourceListCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resources;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResources()];
                    case 1:
                        resources = _a.sent();
                        this.resources.setItems(resources, function (sourceItem, listItem) {
                            return new ResourceListItem_1.ResourceListItem(sourceItem, listItem);
                        });
                        if (resources.length === 0) {
                            this.alert.danger('No Resources were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceListCard.prototype.getResources = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resources;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetResources({
                                            VersionKey: 'Current',
                                            GroupID: this.groupID
                                        })];
                                    case 1:
                                        resources = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, resources];
                }
            });
        });
    };
    return ResourceListCard;
}());
exports.ResourceListCard = ResourceListCard;
//# sourceMappingURL=ResourceListCard.js.map