"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RoleButtonListItemView = void 0;
var tslib_1 = require("tslib");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var ButtonListGroupItemView_1 = require("@jasonbenfield/sharedwebapp/ListGroup/ButtonListGroupItemView");
var RoleButtonListItemView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(RoleButtonListItemView, _super);
    function RoleButtonListItemView() {
        var _this = _super.call(this) || this;
        _this.roleName = _this.addContent(new TextSpanView_1.TextSpanView());
        return _this;
    }
    return RoleButtonListItemView;
}(ButtonListGroupItemView_1.ButtonListGroupItemView));
exports.RoleButtonListItemView = RoleButtonListItemView;
//# sourceMappingURL=RoleButtonListItemView.js.map