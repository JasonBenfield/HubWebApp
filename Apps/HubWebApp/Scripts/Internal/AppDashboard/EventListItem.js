"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.EventListItem = void 0;
var tslib_1 = require("tslib");
var FormattedDate_1 = require("XtiShared/FormattedDate");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var TextCss_1 = require("XtiShared/TextCss");
var EventListItem = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(EventListItem, _super);
    function EventListItem(evt, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(new FormattedDate_1.FormattedDate(evt.TimeOccurred).formatDateTime()));
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(evt.Severity.DisplayText));
        _this.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpan_1.TextSpan(evt.Caption))
            .configure(function (ts) { return ts.setTitle(evt.Caption); });
        _this.addColumn()
            .configure(function (c) { return c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass()); })
            .addContent(new TextSpan_1.TextSpan(evt.Message))
            .configure(function (ts) { return ts.setTitle(evt.Message); });
        return _this;
    }
    return EventListItem;
}(Row_1.Row));
exports.EventListItem = EventListItem;
//# sourceMappingURL=EventListItem.js.map