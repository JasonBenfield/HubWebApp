"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var SelectableListCard_1 = require("../../ListCard/SelectableListCard");
var ResourceGroupListItemViewModel_1 = require("../ResourceGroupListItemViewModel");
var ResourceGroupListCard = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceGroupListCard, _super);
    function ResourceGroupListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Resource Groups were Found') || this;
        _this.hubApi = hubApi;
        _this._resourceSelected = new Events_1.DefaultEvent(_this);
        _this.resourceGroupSelected = _this._resourceSelected.handler();
        vm.title('Resource Groups');
        return _this;
    }
    ResourceGroupListCard.prototype.onItemSelected = function (item) {
        this._resourceSelected.invoke(item.source);
    };
    ResourceGroupListCard.prototype.createItem = function (group) {
        var item = new ResourceGroupListItemViewModel_1.ResourceGroupListItemViewModel(group);
        item.name(group.Name);
        return item;
    };
    ResourceGroupListCard.prototype.getSourceItems = function () {
        return this.hubApi.App.GetResourceGroups();
    };
    return ResourceGroupListCard;
}(SelectableListCard_1.SelectableListCard));
exports.ResourceGroupListCard = ResourceGroupListCard;
//# sourceMappingURL=ResourceGroupListCard.js.map