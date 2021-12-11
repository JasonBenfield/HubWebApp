"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupComponent = void 0;
var tslib_1 = require("tslib");
var Card_1 = require("XtiShared/Card/Card");
var BlockViewModel_1 = require("XtiShared/Html/BlockViewModel");
var TextSpan_1 = require("XtiShared/Html/TextSpan");
var Row_1 = require("XtiShared/Grid/Row");
var ResourceGroupComponent = /** @class */ (function (_super) {
    (0, tslib_1.__extends)(ResourceGroupComponent, _super);
    function ResourceGroupComponent(hubApi, vm) {
        if (vm === void 0) { vm = new BlockViewModel_1.BlockViewModel(); }
        var _this = _super.call(this, vm) || this;
        _this.hubApi = hubApi;
        _this.addCardTitleHeader('Resource Group');
        _this.alert = _this.addCardAlert().alert;
        var listGroup = _this.addListGroup();
        var row = listGroup
            .addItem()
            .addContent(new Row_1.Row());
        _this.groupName = row.addColumn()
            .addContent(new TextSpan_1.TextSpan());
        _this.anonListItem = listGroup.addItem();
        _this.anonListItem.addContent(new Row_1.Row())
            .addColumn()
            .addContent(new TextSpan_1.TextSpan('Anonymous is Allowed'));
        _this.anonListItem.hide();
        return _this;
    }
    ResourceGroupComponent.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceGroupComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var group;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResourceGroup(this.groupID)];
                    case 1:
                        group = _a.sent();
                        this.groupName.setText(group.Name);
                        if (group.IsAnonymousAllowed) {
                            this.anonListItem.show();
                        }
                        else {
                            this.anonListItem.hide();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceGroupComponent.prototype.getResourceGroup = function (groupID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var group;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetResourceGroup({
                                            VersionKey: 'Current',
                                            GroupID: groupID
                                        })];
                                    case 1:
                                        group = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, group];
                }
            });
        });
    };
    return ResourceGroupComponent;
}(Card_1.Card));
exports.ResourceGroupComponent = ResourceGroupComponent;
//# sourceMappingURL=ResourceGroupComponent.js.map