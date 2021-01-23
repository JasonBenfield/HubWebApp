"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageViewModel = void 0;
var tslib_1 = require("tslib");
var tsyringe_1 = require("tsyringe");
var PageViewModel_1 = require("XtiShared/PageViewModel");
var PanelViewModel_1 = require("../Panel/PanelViewModel");
var AppDetailPanelViewModel_1 = require("./AppDetail/AppDetailPanelViewModel");
var template = require("./MainPage.html");
var ResourceGroupPanelViewModel_1 = require("./ResourceGroup/ResourceGroupPanelViewModel");
var ResourcePanelViewModel_1 = require("./Resource/ResourcePanelViewModel");
var ModCategoryPanelViewModel_1 = require("./ModCategory/ModCategoryPanelViewModel");
var MainPageViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(MainPageViewModel, _super);
    function MainPageViewModel() {
        var _this = _super.call(this, template) || this;
        _this.appDetailPanel = new PanelViewModel_1.PanelViewModel(new AppDetailPanelViewModel_1.AppDetailPanelViewModel());
        _this.resourceGroupPanel = new PanelViewModel_1.PanelViewModel(new ResourceGroupPanelViewModel_1.ResourceGroupPanelViewModel());
        _this.resourcePanel = new PanelViewModel_1.PanelViewModel(new ResourcePanelViewModel_1.ResourcePanelViewModel());
        _this.modCategoryPanel = new PanelViewModel_1.PanelViewModel(new ModCategoryPanelViewModel_1.ModCategoryPanelViewModel());
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