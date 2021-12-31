"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var EventListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EventListItemView, _super);
    function EventListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.timeOccurred = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.severity = row.addColumn()
            .addContent(new TextSpanView_1.TextSpanView());
        _this.caption = row.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpanView_1.TextSpanView());
        _this.message = row.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return EventListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.EventListItemView = EventListItemView;
//# sourceMappingURL=EventListItemView.js.map