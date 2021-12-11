"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModCategoryListCard = void 0;
var tslib_1 = require("tslib");
var Row_1 = require("XtiShared/Grid/Row");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var CardAlert_1 = require("XtiShared/Card/CardAlert");
var AlignCss_1 = require("XtiShared/AlignCss");
var ColumnCss_1 = require("XtiShared/ColumnCss");
var Events_1 = require("XtiShared/Events");
var HubTheme_1 = require("../../HubTheme");
var Command_1 = require("XtiShared/Command/Command");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextCss_1 = require("XtiShared/TextCss");
var UserModCategoryListCard = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(UserModCategoryListCard, _super);
    function UserModCategoryListCard(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this._editRequested = new Events_1.SimpleEvent(_this);
        _this.editRequested = _this._editRequested.handler();
        _this.modCategoryComponents = [];
        _this.addCardTitleHeader('User Modifiers');
        _this.alert = _this.addContent(new CardAlert_1.CardAlert()).alert;
        return _this;
    }
    UserModCategoryListCard.prototype.requestEdit = function (userModCategory) {
        this._editRequested.invoke();
    };
    UserModCategoryListCard.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserModCategoryListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var userModCategories, _i, userModCategories_1, userModCategory, header, headerRow, editButton, editCommand, listGroup, cardAlert;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        this.removeModCategories();
                        return [4 /*yield*/, this.getUserModCategories()];
                    case 1:
                        userModCategories = _a.sent();
                        for (_i = 0, userModCategories_1 = userModCategories; _i < userModCategories_1.length; _i++) {
                            userModCategory = userModCategories_1[_i];
                            header = this.addCardHeader();
                            headerRow = header.addContent(new Row_1.Row())
                                .configure(function (row) { return row.addCssFrom(new AlignCss_1.AlignCss().items(function (a) { return a.xs('baseline'); }).cssClass()); });
                            headerRow.addColumn()
                                .addContent(new TextSpan_1.TextSpan(userModCategory.ModCategory.Name));
                            editButton = headerRow.addColumn()
                                .configure(function (col) { return col.setColumnCss(ColumnCss_1.ColumnCss.xs('auto')); })
                                .addContent(HubTheme_1.HubTheme.instance.cardHeader.editButton());
                            editCommand = new Command_1.Command(this.requestEdit.bind(this, userModCategory));
                            editCommand.add(editButton);
                            this.modCategoryComponents.push(header);
                            listGroup = this.addListGroup();
                            this.modCategoryComponents.push(listGroup);
                            listGroup.setItems(userModCategory.Modifiers, function (modifier, listItem) {
                                var row = listItem.addContent(new Row_1.Row());
                                row
                                    .addColumn()
                                    .configure(function (c) {
                                    c.setColumnCss(ColumnCss_1.ColumnCss.xs(4));
                                    c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass());
                                })
                                    .addContent(new TextSpan_1.TextSpan(modifier.ModKey))
                                    .configure(function (ts) { return ts.setTitleFromText(); });
                                row
                                    .addColumn()
                                    .configure(function (c) {
                                    c.addCssFrom(new TextCss_1.TextCss().truncate().cssClass());
                                })
                                    .addContent(new TextSpan_1.TextSpan(modifier.DisplayText))
                                    .configure(function (ts) { return ts.setTitleFromText(); });
                            });
                            if (userModCategory.Modifiers.length === 0) {
                                cardAlert = this.addCardAlert();
                                this.modCategoryComponents.push(cardAlert);
                                cardAlert.alert.danger('No Modifiers were Found for User');
                            }
                        }
                        if (userModCategories.length === 0) {
                            this.alert.danger('No Modifiers were found');
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    UserModCategoryListCard.prototype.removeModCategories = function () {
        for (var _i = 0, _a = this.modCategoryComponents; _i < _a.length; _i++) {
            var modCategory = _a[_i];
            this.removeItem(modCategory);
        }
    };
    UserModCategoryListCard.prototype.getUserModCategories = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var modCategories;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.AppUser.GetUserModCategories(this.userID)];
                                    case 1:
                                        modCategories = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, modCategories];
                }
            });
        });
    };
    return UserModCategoryListCard;
}(Card_1.Card));
exports.UserModCategoryListCard = UserModCategoryListCard;
//# sourceMappingURL=UserModCategoryListCard.js.map