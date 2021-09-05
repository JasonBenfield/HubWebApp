"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestExpandedListItem = void 0;
var tslib_1 = require("tslib");
var FormattedDate_1 = require("XtiShared/FormattedDate");
var Row_1 = require("XtiShared/Grid/Row");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var RequestExpandedListItem = /** @class */ (function (_super) {
    tslib_1.__extends(RequestExpandedListItem, _super);
    function RequestExpandedListItem(req, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        var timeStarted = new FormattedDate_1.FormattedDate(req.TimeStarted).formatDateTime();
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(timeStarted));
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(req.GroupName));
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(req.ActionName));
        _this.addColumn()
            .addContent(new TextSpan_1.TextSpan(req.UserName));
        return _this;
    }
    return RequestExpandedListItem;
}(Row_1.Row));
exports.RequestExpandedListItem = RequestExpandedListItem;
//# sourceMappingURL=RequestExpandedListItem.js.map