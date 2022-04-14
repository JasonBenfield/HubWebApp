"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var AppListCardView_1 = require("./AppListCardView");
var AppListPanelView = /** @class */ (function (_super) {
    tslib_1.__extends(AppListPanelView, _super);
    function AppListPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        _this.appListCard = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill())
            .addContent(new AppListCardView_1.AppListCardView());
        return _this;
    }
    return AppListPanelView;
}(Block_1.Block));
exports.AppListPanelView = AppListPanelView;
//# sourceMappingURL=AppListPanelView.js.map