"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourcePanelViewModel = void 0;
var tslib_1 = require("tslib");
var ComponentTemplate_1 = require("XtiShared/ComponentTemplate");
var CommandOutlineButtonTemplate_1 = require("XtiShared/Templates/CommandOutlineButtonTemplate");
var ComponentViewModel_1 = require("XtiShared/ComponentViewModel");
var ResourceComponentViewModel_1 = require("./ResourceComponentViewModel");
var template = require("./ResourcePanel.html");
var ListCardViewModel_1 = require("../../ListCard/ListCardViewModel");
var ResourcePanelViewModel = /** @class */ (function (_super) {
    tslib_1.__extends(ResourcePanelViewModel, _super);
    function ResourcePanelViewModel() {
        var _this = _super.call(this, new ComponentTemplate_1.ComponentTemplate('resource-panel', template)) || this;
        _this.resourceComponent = new ResourceComponentViewModel_1.ResourceComponentViewModel();
        _this.resourceAccessCard = new ListCardViewModel_1.ListCardViewModel();
        _this.mostRecentRequestListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.mostRecentErrorEventListCard = new ListCardViewModel_1.ListCardViewModel();
        _this.backCommand = CommandOutlineButtonTemplate_1.createCommandOutlineButtonViewModel();
        return _this;
    }
    return ResourcePanelViewModel;
}(ComponentViewModel_1.ComponentViewModel));
exports.ResourcePanelViewModel = ResourcePanelViewModel;
//# sourceMappingURL=ResourcePanelViewModel.js.map