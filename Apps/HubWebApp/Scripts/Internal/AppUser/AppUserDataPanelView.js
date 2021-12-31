"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppUserDataPanelView = void 0;
var tslib_1 = require("tslib");
var Block_1 = require("@jasonbenfield/sharedwebapp/Html/Block");
var FlexColumn_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumn");
var FlexColumnFill_1 = require("@jasonbenfield/sharedwebapp/Html/FlexColumnFill");
var MessageAlertView_1 = require("@jasonbenfield/sharedwebapp/MessageAlertView");
var AppUserDataPanelView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(AppUserDataPanelView, _super);
    function AppUserDataPanelView() {
        var _this = _super.call(this) || this;
        _this.height100();
        var flexColumn = _this.addContent(new FlexColumn_1.FlexColumn());
        var flexFill = flexColumn.addContent(new FlexColumnFill_1.FlexColumnFill());
        _this.alert = flexFill.addContent(new MessageAlertView_1.MessageAlertView());
        return _this;
    }
    return AppUserDataPanelView;
}(Block_1.Block));
exports.AppUserDataPanelView = AppUserDataPanelView;
//# sourceMappingURL=AppUserDataPanelView.js.map