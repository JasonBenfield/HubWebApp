"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItemView = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var ListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ListGroupItemView");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var EventListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EventListItemView, _super);
    function EventListItemView() {
        var _this = _super.call(this) || this;
        var row = _this.addContent(new Row_1.Row());
        _this.timeOccurred = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.severity = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.caption = row.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpan_1.TextSpan());
        _this.caption.syncTitleWithText();
        _this.message = row.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpan_1.TextSpan());
        _this.message.syncTitleWithText();
        return _this;
    }
    EventListItemView.prototype.setTimeOccurred = function (timeOccurred) { this.timeOccurred.setText(timeOccurred); };
    EventListItemView.prototype.setSeverity = function (severity) { this.severity.setText(severity); };
    EventListItemView.prototype.setCaption = function (caption) { this.caption.setText(caption); };
    EventListItemView.prototype.setMessage = function (message) { this.message.setText(message); };
    return EventListItemView;
}(ListGroupItemView_1.ListGroupItemView));
exports.EventListItemView = EventListItemView;
//# sourceMappingURL=EventListItemView.js.map