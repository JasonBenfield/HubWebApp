"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentErrorEventListCard = void 0;
var tslib_1 = require("tslib");
var EventListItem_1 = require("../EventListItem");
var EventListItemViewModel_1 = require("../EventListItemViewModel");
var ListCard_1 = require("../../ListCard/ListCard");
var MostRecentErrorEventListCard = /** @class */ (function (_super) {
    tslib_1.__extends(MostRecentErrorEventListCard, _super);
    function MostRecentErrorEventListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Errors were Found') || this;
        _this.hubApi = hubApi;
        vm.title('Most Recent Errors');
        return _this;
    }
    MostRecentErrorEventListCard.prototype.setResourceID = function (resourceID) {
        this.resourceID = resourceID;
    };
    MostRecentErrorEventListCard.prototype.createItem = function (evt) {
        var item = new EventListItemViewModel_1.EventListItemViewModel();
        new EventListItem_1.EventListItem(item, evt);
        return item;
    };
    MostRecentErrorEventListCard.prototype.getSourceItems = function () {
        return this.hubApi.Resource.GetMostRecentErrorEvents({ ResourceID: this.resourceID, HowMany: 10 });
    };
    return MostRecentErrorEventListCard;
}(ListCard_1.ListCard));
exports.MostRecentErrorEventListCard = MostRecentErrorEventListCard;
//# sourceMappingURL=MostRecentErrorEventListCard.js.map