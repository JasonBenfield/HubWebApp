"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserListCard = void 0;
var tslib_1 = require("tslib");
var Events_1 = require("XtiShared/Events");
var SelectableListCard_1 = require("../../ListCard/SelectableListCard");
var UserListItemViewModel_1 = require("./UserListItemViewModel");
var UserListCard = /** @class */ (function (_super) {
    tslib_1.__extends(UserListCard, _super);
    function UserListCard(vm, hubApi) {
        var _this = _super.call(this, vm, 'No Users were Found') || this;
        _this.hubApi = hubApi;
        _this._userSelected = new Events_1.DefaultEvent(_this);
        _this.userSelected = _this._userSelected.handler();
        vm.title('Users');
        return _this;
    }
    UserListCard.prototype.onItemSelected = function (item) {
        this._userSelected.invoke(item.source);
    };
    UserListCard.prototype.createItem = function (sourceItem) {
        var item = new UserListItemViewModel_1.UserListItemViewModel(sourceItem);
        item.userName(sourceItem.UserName);
        item.name(sourceItem.Name);
        return item;
    };
    UserListCard.prototype.getSourceItems = function () {
        return this.hubApi.Users.GetUsers();
    };
    return UserListCard;
}(SelectableListCard_1.SelectableListCard));
exports.UserListCard = UserListCard;
//# sourceMappingURL=UserListCard.js.map