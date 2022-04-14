"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupComponent = void 0;
var tslib_1 = require("tslib");
var CardAlert_1 = require("@jasonbenfield/sharedwebapp/Card/CardAlert");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var ResourceGroupComponent = /** @class */ (function () {
    function ResourceGroupComponent(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Resource Group', this.view.titleHeader);
        this.alert = new CardAlert_1.CardAlert(this.view.alert).alert;
        this.groupName = new TextBlock_1.TextBlock('', this.view.groupName);
        this.view.hideAnonMessage();
    }
    ResourceGroupComponent.prototype.setGroupID = function (groupID) {
        this.groupID = groupID;
    };
    ResourceGroupComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var group;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResourceGroup(this.groupID)];
                    case 1:
                        group = _a.sent();
                        this.groupName.setText(group.Name);
                        if (group.IsAnonymousAllowed) {
                            this.view.showAnonMessage();
                        }
                        else {
                            this.view.hideAnonMessage();
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceGroupComponent.prototype.getResourceGroup = function (groupID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var group;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
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
}());
exports.ResourceGroupComponent = ResourceGroupComponent;
//# sourceMappingURL=ResourceGroupComponent.js.map