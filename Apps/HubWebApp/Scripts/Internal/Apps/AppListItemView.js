"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var LinkListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/LinkListGroupItemView");
var LinkListItemViewModel_1 = require("@jasonbenfield/sharedwebapp/ListGroup/LinkListItemViewModel");
var AppListItemView = /** @class */ (function (_super) {
    tslib_1.__extends(AppListItemView, _super);
    function AppListItemView() {
        var _this = _super.call(this, new LinkListItemViewModel_1.LinkListItemViewModel()) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.appName = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.appTitle = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.appType = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return AppListItemView;
}(LinkListGroupItemView_1.LinkListGroupItemView));
exports.AppListItemView = AppListItemView;
//# sourceMappingURL=AppListItemView.js.map