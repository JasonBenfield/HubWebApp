"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceComponent = void 0;
var tslib_1 = require("tslib");
var Alert_1 = require("XtiShared/Alert");
var ResourceResultType_1 = require("../../../Hub/Api/ResourceResultType");
var ResourceComponent = /** @class */ (function () {
    function ResourceComponent(vm, hubApi) {
        this.vm = vm;
        this.hubApi = hubApi;
        this.alert = new Alert_1.Alert(this.vm.alert);
    }
    ResourceComponent.prototype.setResourceID = function (resourceID) {
        this.resourceID = resourceID;
        this.vm.resourceName('');
    };
    ResourceComponent.prototype.refresh = function () {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var resource, resultType, resultTypeText;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResource(this.resourceID)];
                    case 1:
                        resource = _a.sent();
                        this.vm.resourceName(resource.Name);
                        this.vm.isAnonymousAllowed(resource.IsAnonymousAllowed);
                        resultType = ResourceResultType_1.ResourceResultType.values.value(resource.ResultType.Value);
                        if (resultType.equalsAny(ResourceResultType_1.ResourceResultType.values.None, ResourceResultType_1.ResourceResultType.values.Json)) {
                            resultTypeText = '';
                        }
                        else {
                            resultTypeText = resultType.DisplayText;
                        }
                        this.vm.resultType(resultTypeText);
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceComponent.prototype.getResource = function (resourceID) {
        return tslib_1.__awaiter(this, void 0, void 0, function () {
            var resource;
            var _this = this;
            return tslib_1.__generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return tslib_1.__awaiter(_this, void 0, void 0, function () {
                            return tslib_1.__generator(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Resource.GetResource(resourceID)];
                                    case 1:
                                        resource = _a.sent();
                                        return [2 /*return*/];
                                }
                            });
                        }); })];
                    case 1:
                        _a.sent();
                        return [2 /*return*/, resource];
                }
            });
        });
    };
    return ResourceComponent;
}());
exports.ResourceComponent = ResourceComponent;
//# sourceMappingURL=ResourceComponent.js.map