"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MainPageView = void 0;
var PaddingCss_1 = require("@jasonbenfield/sharedwebapp/PaddingCss");
var AppDetailPanelView_1 = require("./AppDetail/AppDetailPanelView");
var ModCategoryPanelView_1 = require("./ModCategory/ModCategoryPanelView");
var ResourcePanelView_1 = require("./Resource/ResourcePanelView");
var ResourceGroupPanelView_1 = require("./ResourceGroup/ResourceGroupPanelView");
var MainPageView = /** @class */ (function () {
    function MainPageView(page) {
        this.page = page;
        this.page.content.setPadding(PaddingCss_1.PaddingCss.top(3));
        this.appDetailPanel = this.page.addContent(new AppDetailPanelView_1.AppDetailPanelView());
        this.resourceGroupPanel = this.page.addContent(new ResourceGroupPanelView_1.ResourceGroupPanelView());
        this.resourcePanel = this.page.addContent(new ResourcePanelView_1.ResourcePanelView());
        this.modCategoryPanel = this.page.addContent(new ModCategoryPanelView_1.ModCategoryPanelView());
    }
    return MainPageView;
}());
exports.MainPageView = MainPageView;
//# sourceMappingURL=MainPageView.js.map