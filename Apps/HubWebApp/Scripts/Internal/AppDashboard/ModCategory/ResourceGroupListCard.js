"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListCard = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var ResourceGroupListItem_1 = require("../ResourceGroupListItem");
var ResourceGroupListCard = /** @class */ (function () {
    function ResourceGroupListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._resourceSelected = new Events_1.DefaultEvent(this);
        this.resourceGroupSelected = this._resourceSelected.handler();
        new TextBlock_1.TextBlock('Resource Groups', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.requests = new ListGroup_1.ListGroup(this.view.requests);
    }
    ResourceGroupListCard.prototype.onItemSelected = function (item) {
        this._resourceSelected.invoke(item.group);
    };
    ResourceGroupListCard.prototype.setModCategoryID = function (modCategoryID) {
        this.modCategoryID = modCategoryID;
    };
    ResourceGroupListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resourceGroups;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResourceGroups()];
                    case 1:
                        resourceGroups = _a.sent();
                        this.requests.setItems(resourceGroups, function (sourceItem, listItem) {
                            return new ResourceGroupListItem_1.ResourceGroupListItem(sourceItem, listItem);
                        });
                        if (resourceGroups.length === 0) {
                            this.alert.danger('No Resource Groups were Found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceGroupListCard.prototype.getResourceGroups = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resourceGroup;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ModCategory.GetResourceGroups(this.modCategoryID)];
                                    case 1:
                                        resourceGroup = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, resourceGroup];
                }
            });
        });
    };
    return ResourceGroupListCard;
}());
exports.ResourceGroupListCard = ResourceGroupListCard;
//# sourceMappingURL=ResourceGroupListCard.js.map