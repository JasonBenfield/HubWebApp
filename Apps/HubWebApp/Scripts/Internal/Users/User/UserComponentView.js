"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserComponentView = void 0;
var tslib_1 = require("tslib");
var CardView_1 = require("@jasonbenfield/sharedwebapp/Card/CardView");
var ColumnCss_1 = require("@jasonbenfield/sharedwebapp/ColumnCss");
var Row_1 = require("@jasonbenfield/sharedwebapp/Grid/Row");
var TextSpanView_1 = require("@jasonbenfield/sharedwebapp/Html/TextSpanView");
var TextValueFormGroupView_1 = require("@jasonbenfield/sharedwebapp/Html/TextValueFormGroupView");
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
            .addContent(new TextSpanView_1.TextSpanView())
            .configure(function (ts) { return ts.setText('User'); });
        _this.editButton = headerRow.addColumn()
            .configure(function (c) { return c.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
            .addContent(HubTheme_1.HubTheme.instance.cardHeader.editButton());
        _this.alert = _this.addCardAlert().alert;
        var body = _this.addCardBody();
        _this.userName = _this.addBodyRow(body);
        _this.fullName = _this.addBodyRow(body);
        _this.email = _this.addBodyRow(body);
        return _this;
    }
    UserComponentView.prototype.addBodyRow = function (body) {
        var formGroup = body.addContent(new TextValueFormGroupView_1.TextValueFormGroupView());
        formGroup.captionColumn.setColumnCss(ColumnCss_1.ColumnCss.xs(4));
        formGroup.valueColumn.setTextCss(new TextCss_1.TextCss().truncate());
        return formGroup;
    };
    return UserComponentView;
}(CardView_1.CardView));
exports.UserComponentView = UserComponentView;
//# sourceMappingURL=UserComponentView.js.map