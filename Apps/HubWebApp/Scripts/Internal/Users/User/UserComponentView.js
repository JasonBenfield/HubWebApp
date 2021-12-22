"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var FormGroupView_1 = require("@jasonbenfield/sharedwebapp/Html/FormGroupView");
var TextSpan_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpan");
var TextCss_1 = require("@jasonbenfield/sharedwebapp/TextCss");
var HubTheme_1 = require("../../HubTheme");
var UserComponentView = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserComponentView, _super);
    function UserComponentView() {
        var _this = _super.call(this) || this;
        _this.setName(UserComponentView.name);
        var headerRow = _this.addCardHeader()
            .addContent(new Row_1.Row());
        headerRow.addColumn()
            .addContent(new TextSpan_1.TextSpan('User'));
        _this.editButton = headerRow.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(HubTheme_1.HubTheme.instance.cardHeader.editButton());
        _this.alert = _this.addCardAlert().alert;
        var body = _this.addCardBody();
        _this.userName = _this.addBodyRow(body, 'User Name');
        _this.userName.syncTitleWithText();
        _this.fullName = _this.addBodyRow(body, 'Name');
        _this.fullName.syncTitleWithText();
        _this.email = _this.addBodyRow(body, 'Email');
        _this.email.syncTitleWithText();
        return _this;
    }
    UserComponentView.prototype.setUserName = function (userName) { this.userName.setText(userName); };
    UserComponentView.prototype.setFullName = function (fullName) { this.fullName.setText(fullName); };
    UserComponentView.prototype.setEmail = function (email) { this.email.setText(email); };
    UserComponentView.prototype.addBodyRow = function (body, caption) {
        var formGroup = body.addContent(new FormGroupView_1.FormGroupView());
        formGroup.captionColumn.addContent(new TextSpan_1.TextSpan(caption));
        formGroup.captionColumn.setColumnCss(ColumnCss_1.ColumnCss.xs(4));
        formGroup.valueColumn.setTextCss(new TextCss_1.TextCss().truncate());
        return formGroup.valueColumn.addContent(new TextSpan_1.TextSpan())
            .configure(function (ts) { return ts.addCssName('form-control-plaintext'); });
    };
    return UserComponentView;
}(CardView_1.CardView));
exports.UserComponentView = UserComponentView;
//# sourceMappingURL=UserComponentView.js.map