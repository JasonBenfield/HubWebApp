"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MostRecentRequestListCard = void 0;
var tslib_1 = require("tslib");
var ListCard_1 = require("../ListCard");
var RequestExpandedListItem_1 = require("../RequestExpandedListItem");
var RequestExpandedListItemViewModel_1 = require("../RequestExpandedListItemViewModel");
var MostRecentRequestListCard = /** @class */ (function (_super) {
    tslib_1.__extends(MostRecentRequestListCard, _super);
    function MostRecentRequestListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Requests were Found') || this;
        _this.hubApi = hubApi;
        vm.title('Most Recent Requests');
        return _this;
    }
    MostRecentRequestListCard.prototype.setResourceID = function (resourceID) {
        this.resourceID = resourceID;
    };
    MostRecentRequestListCard.prototype.createItem = function (request) {
        var item = new RequestExpandedListItemViewModel_1.RequestExpandedListItemViewModel();
        new RequestExpandedListItem_1.RequestExpandedListItem(item, request);
        return item;
    };
    MostRecentRequestListCard.prototype.getSourceItems = function () {
        return this.hubApi.Resource.GetMostRecentRequests({ ResourceID: this.resourceID, HowMany: 10 });
    };
    return MostRecentRequestListCard;
}(ListCard_1.ListCard));
exports.MostRecentRequestListCard = MostRecentRequestListCard;
//# sourceMappingURL=MostRecentRequestListCard.js.map