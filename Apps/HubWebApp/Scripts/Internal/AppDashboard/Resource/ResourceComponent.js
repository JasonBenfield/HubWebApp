"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ResourceComponent = void 0;
var tslib_1 = require("tslib");
var TextBlock_1 = require("@jasonbenfield/sharedwebapp/Html/TextBlock");
var MessageAlert_1 = require("@jasonbenfield/sharedwebapp/MessageAlert");
var ResourceResultType_1 = require("../../../Hub/Api/ResourceResultType");
var ResourceComponent = /** @class */ (function () {
    function ResourceComponent(hubApi, view) {
        this.hubApi = hubApi;
        this.view = view;
        new TextBlock_1.TextBlock('Resource', this.view.titleHeader);
        this.alert = new MessageAlert_1.MessageAlert(this.view.alert);
        this.resourceName = new TextBlock_1.TextBlock('', view.resourceName);
        this.resultType = new TextBlock_1.TextBlock('', view.resultType);
    }
    ResourceComponent.prototype.setResourceID = function (resourceID) {
        this.resourceID = resourceID;
        this.resourceName.setText('');
        this.resultType.setText('');
        this.view.hideAnon();
    };
    ResourceComponent.prototype.refresh = function () {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resource, resultType, resultTypeText;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.getResource(this.resourceID)];
                    case 1:
                        resource = _a.sent();
                        this.resourceName.setText(resource.Name);
                        if (resource.IsAnonymousAllowed) {
                            this.view.showAnon();
                        }
                        else {
                            this.view.hideAnon();
                        }
                        resultType = ResourceResultType_1.ResourceResultType.values.value(resource.ResultType.Value);
                        if (resultType.equalsAny(ResourceResultType_1.ResourceResultType.values.None, ResourceResultType_1.ResourceResultType.values.Json)) {
                            resultTypeText = '';
                        }
                        else {
                            resultTypeText = resultType.DisplayText;
                        }
                        this.resultType.setText(resultTypeText);
                        return [2 /*return*/];
                }
            });
        });
    };
    ResourceComponent.prototype.getResource = function (resourceID) {
        return (0, tslib_1.__awaiter)(this, void 0, void 0, function () {
            var resource;
            var _this = this;
            return (0, tslib_1.__generator)(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.alert.infoAction('Loading...', function () { return (0, tslib_1.__awaiter)(_this, void 0, void 0, function () {
                            return (0, tslib_1.__generator)(this, function (_a) {
                                switch (_a.label) {
                                    case 0: return [4 /*yield*/, this.hubApi.Resource.GetResource({
                                            VersionKey: 'Current',
                                            ResourceID: resourceID
                                        })];
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