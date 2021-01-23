"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceGroupComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var ResourceGroupComponent = /** @class */ (function () {
    function ResourceGroupComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.alert = new Alert_1.Alert(this.vm.alert);
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
                        this.vm.groupName(group.Name);
                        this.vm.isAnonymousAllowed(group.IsAnonymousAllowed);
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
                                    case 0: return [4 /*yield*/, this.hubApi.ResourceGroup.GetResourceGroup(groupID)];
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