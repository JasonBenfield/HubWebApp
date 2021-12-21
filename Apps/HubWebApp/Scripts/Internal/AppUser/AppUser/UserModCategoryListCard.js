"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModCategoryListCard = void 0;
var tslib_1 = require("tslib");
var CardTitleHeader_1 = require("@jasonbenfield/sharedwebapp/Card/CardTitleHeader");
var Events_1 = require("@jasonbenfield/sharedwebapp/Events");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var UserModCategoryListCard = /** @class */ (function () {
    function UserModCategoryListCard(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        this._editRequested = new Events_1.SimpleEvent(this);
        this.editRequested = this._editRequested.handler();
        new CardTitleHeader_1.CardTitleHeader('User Modifiers', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
    }
    UserModCategoryListCard.prototype.requestEdit = function (userModCategory) {
        this._editRequested.invoke();
    };
    UserModCategoryListCard.prototype.setUserID = function (userID) {
        this.userID = userID;
    };
    UserModCategoryListCard.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var userModCategories, _i, userModCategories_1, userModCategory;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getUserModCategories()];
                    case 1:
                        userModCategories = _a.sent();
                        for (_i = 0, userModCategories_1 = userModCategories; _i < userModCategories_1.length; _i++) {
                            userModCategory = userModCategories_1[_i];
                            //    let header = this.addCardHeader();
                            //    let headerRow = header.addContent(new Row())
                            //        .configure(row => row.addCssFrom(new AlignCss().items(a => a.xs('baseline')).cssClass()));
                            //    headerRow.addColumn()
                            //        .addContent(new TextSpan(userModCategory.ModCategory.Name));
                            //    let editButton = headerRow.addColumn()
                            //        .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
                            //        .addContent(HubTheme.instance.cardHeader.editButton());
                            //    let editCommand = new Command(this.requestEdit.bind(this, userModCategory));
                            //    editCommand.add(editButton);
                            //    this.modCategoryComponents.push(header);
                            //    let listGroup = this.addListGroup();
                            //    this.modCategoryComponents.push(listGroup);
                            //    listGroup.setItems(
                            //        userModCategory.Modifiers,
                            //        (modifier, listItem) => {
                            //            let row = listItem.addContent(new Row());
                            //            row
                            //                .addColumn()
                            //                .configure(c => {
                            //                    c.setColumnCss(ColumnCss.xs(4));
                            //                    c.addCssFrom(new TextCss().truncate().cssClass());
                            //                })
                            //                .addContent(new TextSpan(modifier.ModKey))
                            //                .configure(ts => ts.setTitleFromText());
                            //            row
                            //                .addColumn()
                            //                .configure(c => {
                            //                    c.addCssFrom(new TextCss().truncate().cssClass());
                            //                })
                            //                .addContent(new TextSpan(modifier.DisplayText))
                            //                .configure(ts => ts.setTitleFromText());
                            //        }
                            //    );
                            //    if (userModCategory.Modifiers.length === 0) {
                            //        let cardAlert = this.addCardAlert();
                            //        this.modCategoryComponents.push(cardAlert);
                            //        cardAlert.alert.danger('No Modifiers were Found for User');
                            //    }
                            //}
                            //if (userModCategories.length === 0) {
                            //    this.alert.danger('No Modifiers were found');
                            //}
                        }
                        return [2 /*return*/];
                }
            });
        });
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
}());
exports.UserModCategoryListCard = UserModCategoryListCard;
//# sourceMappingURL=UserModCategoryListCard.js.map