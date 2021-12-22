"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var UserComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserComponentView, _super);
    function UserComponentView() {
        var _this = _super.call(this) || this;
        _this.titleHeader = _this.addCardTitleHeader();
        _this.alert = _this.addCardAlert().alert;
        _this.cardBody = _this.addCardBody();
        var row = _this.cardBody.addContent(new Row_1.Row());
        _this.userName = row.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(new TextSpan_1.TextSpan('User'));
        _this.fullName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.cardBody.hide();
        return _this;
    }
    UserComponentView.prototype.setUserName = function (userName) { this.userName.setText(userName); };
    UserComponentView.prototype.setFullName = function (fullName) { this.fullName.setText(fullName); };
    UserComponentView.prototype.showCardBody = function () { this.cardBody.show(); };
    UserComponentView.prototype.hideCardBody = function () { this.cardBody.hide(); };
    return UserComponentView;
}(CardView_1.CardView));
exports.UserComponentView = UserComponentView;
//# sourceMappingURL=UserComponentView.js.map