"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListCard = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ListGroup_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroup");
var ResourceGroupListItem_1 = require("../ResourceGroupListItem");
var ResourceGroupListCard = /** @class */ (function () {
    function ResourceGroupListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Resource Groups', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.resourceGroups = new ListGroup_1.ListGroup(this.view.resourceGroups);
        this.resourceGroupClicked = this.resourceGroups.itemClicked;
    }
    ResourceGroupListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resourceGroups;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResourceGroups()];
                    case 1:
                        resourceGroups = _a.sent();
                        this.resourceGroups.setItems(resourceGroups, function (sourceItem, listItem) {
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
        var _this = this;
        return this.alert.infoAction('Loading...', function () { return _this.hubApi.App.GetResourceGroups(); });
    };
    return ResourceGroupListCard;
}());
exports.ResourceGroupListCard = ResourceGroupListCard;
//# sourceMappingURL=ResourceGroupListCard.js.map