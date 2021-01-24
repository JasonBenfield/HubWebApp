"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("../../../Imports/Shared/Events");
var SelectableListCard_1 = require("../ListCard/SelectableListCard");
var AppListItemViewModel_1 = require("./AppListItemViewModel");
var AppListCard = /** @class */ (function (_super) {
    tslib_1.__extends(AppListCard, _super);
    function AppListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Apps were Found') || this;
        _this.hubApi = hubApi;
        _this._appSelected = new Events_1.DefaultEvent(_this);
        _this.appSelected = _this._appSelected.handler();
        vm.componentName('selectable-list-card');
        vm.title('Apps');
        return _this;
    }
    AppListCard.prototype.onItemSelected = function (item) {
        this._appSelected.invoke(item.source);
    };
    AppListCard.prototype.createItem = function (sourceItem) {
        var item = new AppListItemViewModel_1.AppListItemViewModel(sourceItem);
        item.appName(sourceItem.AppName);
        item.title(sourceItem.Title);
        item.type(sourceItem.Type.DisplayText);
        return item;
    };
    AppListCard.prototype.getSourceItems = function () {
        return this.hubApi.Apps.All();
    };
    return AppListCard;
}(SelectableListCard_1.SelectableListCard));
exports.AppListCard = AppListCard;
//# sourceMappingURL=AppListCard.js.map