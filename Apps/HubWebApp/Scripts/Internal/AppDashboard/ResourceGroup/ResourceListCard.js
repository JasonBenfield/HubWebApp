"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var ResourceResultType_1 = require("../../../Hub/Api/ResourceResultType");
var SelectableListCard_1 = require("../../ListCard/SelectableListCard");
var ResourceListItemViewModel_1 = require("./ResourceListItemViewModel");
var ResourceListCard = /** @class */ (function (_super) {
    tslib_1.__extends(ResourceListCard, _super);
    function ResourceListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Resources were Found') || this;
        _this.hubApi = hubApi;
        _this._resourceSelected = new Events_1.DefaultEvent(_this);
        _this.resourceSelected = _this._resourceSelected.handler();
        vm.title('Resources');
        return _this;
    }
    ResourceListCard.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceListCard.prototype.onItemSelected = function (item) {
        this._resourceSelected.invoke(item.source);
    };
    ResourceListCard.prototype.createItem = function (sourceItem) {
        var item = new ResourceListItemViewModel_1.ResourceListItemViewModel(sourceItem);
        item.name(sourceItem.Name);
        var resultType = ResourceResultType_1.ResourceResultType.values.value(sourceItem.ResultType.Value);
        var resultTypeText;
        if (resultType.equalsAny(ResourceResultType_1.ResourceResultType.values.None, ResourceResultType_1.ResourceResultType.values.Json)) {
            resultTypeText = '';
        }
        else {
            resultTypeText = resultType.DisplayText;
        }
        item.resultType(resultTypeText);
        return item;
    };
    ResourceListCard.prototype.getSourceItems = function () {
        return this.hubApi.ResourceGroup.GetResources(this.groupID);
    };
    return ResourceListCard;
}(SelectableListCard_1.SelectableListCard));
exports.ResourceListCard = ResourceListCard;
//# sourceMappingURL=ResourceListCard.js.map