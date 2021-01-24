"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageViewModel = void 0;
var tslib_1 = require("tslib");
var tsyringe_1 = require("tsyringe");
var PageViewModel_1 = require("XtiShared/PageViewModel");
var template = require("./MainPage.html");
var UserListPanelViewModel_1 = require("./UserListPanelViewModel");
var MainPageViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(MainPageViewModel, _super);
    function MainPageViewModel() {
        var _this = _super.call(this, template) || this;
        _this.userListPanel = new UserListPanelViewModel_1.UserListPanelViewModel();
        return _this;
    }
    MainPageViewModel = tslib_1.__decorate([
        tsyringe_1.singleton(),
        tslib_1.__metadata("design:paramtypes", [])
    ], MainPageViewModel);
    return MainPageViewModel;
}(PageViewModel_1.PageViewModel));
exports.MainPageViewModel = MainPageViewModel;
//# sourceMappingURL=MainPageViewModel.js.map